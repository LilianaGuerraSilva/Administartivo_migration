VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsFacturaRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsFacturaRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reportes Factura"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO() As Integer
   CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO = 14
End Function

Private Function CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL() As Integer
   CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL = 15
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MONTO() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MONTO = 32
End Function

Private Function CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_DETALLADO() As Integer
   CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_DETALLADO = 33
End Function

Private Function CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO() As Integer
   CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO = 34
End Function

Private Function CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO_ARTICULO() As Integer
   CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO_ARTICULO = 35
End Function

Private Function CM_RPT_INFORME_DE_NOTAS_DE_CREDITO_ASOCIADAS_A_FACTURAS() As Integer
   CM_RPT_INFORME_DE_NOTAS_DE_CREDITO_ASOCIADAS_A_FACTURAS = 40
End Function

Private Function CM_RPT_INFORME_DE_FACTURACION_CON_OTROS_CARGOS_Y_DESCUENTOS() As Integer
   CM_RPT_INFORME_DE_FACTURACION_CON_OTROS_CARGOS_Y_DESCUENTOS = 41
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_X_CONDICION_PAGO() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_X_CONDICION_PAGO = 44
End Function

Private Function CM_RPT_DIRECCIONES_DE_DESPACHO_X_FACTURA() As Integer
   CM_RPT_DIRECCIONES_DE_DESPACHO_X_FACTURA = 45
End Function

Private Function CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_VENDEDOR() As Integer
   CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_VENDEDOR = 46
End Function

Private Function CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_VENDEDOR() As Integer
   CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_VENDEDOR = 47
End Function

Private Function CM_RPT_FACTURAS_X_DIA() As Integer
   CM_RPT_FACTURAS_X_DIA = 48
End Function

Private Function CM_RPT_NOTAS_DE_CREDITO_ENTRE_FECHAS_POR_VENDEDOR() As Integer
   CM_RPT_NOTAS_DE_CREDITO_ENTRE_FECHAS_POR_VENDEDOR = 49
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MUESTRA_MONTO() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MUESTRA_MONTO = 50
End Function

Private Function CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS() As Integer
   CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS = 0
End Function

Private Function CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS_POR_MAQUINA_FISCAL() As Integer
   CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS_POR_MAQUINA_FISCAL = 1
End Function

Private Function CM_RPT_SERVICIOS_POR_VEHICULO() As Integer
   CM_RPT_SERVICIOS_POR_VEHICULO = 51
End Function

Private Function CM_RPT_SERVICIOS_DE_VEHICULOS_POR_CATEGORIA() As Integer
   CM_RPT_SERVICIOS_DE_VEHICULOS_POR_CATEGORIA = 52
End Function

Private Function CM_RPT_HORAS_POR_SERVICIO() As Integer
   CM_RPT_HORAS_POR_SERVICIO = 53
End Function

Private Function CM_RPT_COMISION_DE_SERVICIO() As Integer
   CM_RPT_COMISION_DE_SERVICIO = 54
End Function

Private Function CM_RPT_LISTADO_DE_VEHICULOS() As Integer
   CM_RPT_LISTADO_DE_VEHICULOS = 55
End Function

Private Function CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_POR_ARTICULO() As Integer
   CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_POR_ARTICULO = 56
End Function

Private Function CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA() As Integer
   CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA = 57
End Function

Private Function CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MUESTRA_MONTO() As Integer
   CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MUESTRA_MONTO = 58
End Function

Private Function CM_RPT_FACTURA_ENTRE_FECHAS_POR_LINEA() As Integer
   CM_RPT_FACTURA_ENTRE_FECHAS_POR_LINEA = 59
End Function

Private Function CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MONTO_IVA() As Integer
   CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MONTO_IVA = 60
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_CON_SERIAL() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_CON_SERIAL = 61
End Function

Private Function CM_RPT_FACTURAS_POR_TALONARIO() As Integer
   CM_RPT_FACTURAS_POR_TALONARIO = 62
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHA_POR_CAMPOS_DEFINIBLES() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHA_POR_CAMPOS_DEFINIBLES = 63
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO = 64
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL = 65
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO_X_VENDEDOR() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO_X_VENDEDOR = 66
End Function

Private Function CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL_X_VENDEDOR() As Integer
   CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL_X_VENDEDOR = 67
End Function

Private Function CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_CLIENTE() As Integer
   CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_CLIENTE = 68
End Function

Private Function CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_CLIENTE() As Integer
   CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_CLIENTE = 69
End Function

Private Function CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_SEGUN_LEY_POR_ARTICULO() As Integer
   CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_SEGUN_LEY_POR_ARTICULO = 70
End Function

Private Function CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN() As Integer
   CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN = 71
End Function

Private Function CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN_RESUMIDO() As Integer
   CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN_RESUMIDO = 72
End Function

Private Function fNombreFileDeImpresionDelReporteDeFactura(ByVal valReporte As Integer, _
               Optional ByVal valTalonario As enum_Talonario = enum_Talonario.eTa_TALONARIO1) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MONTO: nombreFileXML = "rpxFacturacionEntreFechasMontoOriginalTipoDeArticulo"
      Case CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_DETALLADO: nombreFileXML = "rpxFacturacionPorArticuloYPorVendedorDetallado"
      Case CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO: nombreFileXML = "rpxFactPorArticuloYPorVendedorResumido"
      Case CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO_ARTICULO: nombreFileXML = "rpxFactPorArticuloYPorVendedorResumidoXArticulos"
      Case CM_RPT_INFORME_DE_NOTAS_DE_CREDITO_ASOCIADAS_A_FACTURAS: nombreFileXML = "rpxNotasdeCreditoAsociadasAFacturas"
      Case CM_RPT_INFORME_DE_FACTURACION_CON_OTROS_CARGOS_Y_DESCUENTOS: nombreFileXML = "rpxInformeFacturaConOtrosCargosYDescuentos"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_X_CONDICION_PAGO: nombreFileXML = "rpxFacturacionEntreFechasCondicionesDePago"
      Case CM_RPT_DIRECCIONES_DE_DESPACHO_X_FACTURA: nombreFileXML = "rpxDireccionesDeDespachoXFactura"
      Case CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO: nombreFileXML = "rpxFacturacionEntreFechasContacto"
      Case CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL: nombreFileXML = "rpxFacturacionEntreFechasMontoOriginal"
      Case CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_VENDEDOR: nombreFileXML = "rpxFacturacionEntreFechasContactoXVendedor"
      Case CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_VENDEDOR: nombreFileXML = "rpxFacturacionEntreFechasMontoOriginalXVendedor"
      Case CM_RPT_FACTURAS_X_DIA: nombreFileXML = "rpxFacturacionPorDia"
      Case CM_RPT_NOTAS_DE_CREDITO_ENTRE_FECHAS_POR_VENDEDOR: nombreFileXML = "rpxNotasDeCreditoEntreFechasPorVendedor"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MUESTRA_MONTO: nombreFileXML = "rpxFacturacionEntreFechasTipoDeArticuloMuestraMonto"
      Case CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS: nombreFileXML = "rpxResumenDiarioEntreFechas"
      Case CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS_POR_MAQUINA_FISCAL: nombreFileXML = "rpxResumenDiarioEntreFechasPorMaquinaFiscal"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_POR_ARTICULO: nombreFileXML = "rpxFacturaEntreFechasConCostosPorArticulo"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_SEGUN_LEY_POR_ARTICULO: nombreFileXML = "rpxFacturaEntreFechasConCostosSegunLeyPorArticulo"
      Case CM_RPT_SERVICIOS_POR_VEHICULO: nombreFileXML = "rpxServiciosXVehiculo"
      Case CM_RPT_SERVICIOS_DE_VEHICULOS_POR_CATEGORIA: nombreFileXML = "rpxServiciosDeVehiculosXCategoria"
      Case CM_RPT_HORAS_POR_SERVICIO: nombreFileXML = "rpxHorasDeServicios"
      Case CM_RPT_COMISION_DE_SERVICIO: nombreFileXML = "rpxComisionDeServiciosPorMecanico"
      Case CM_RPT_LISTADO_DE_VEHICULOS: nombreFileXML = "rpxListadoDeVehiculosPorPropietario"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA: nombreFileXML = "rpxFacturacionEntreFechasPorCategoria"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MUESTRA_MONTO: nombreFileXML = "rpxFacturacionEntreFechasPorCategoriaMuestraMonto"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_LINEA: nombreFileXML = "rpxFacturacionEntreFechasPorLineaDeProducto"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MONTO_IVA: nombreFileXML = "rpxFacturacionEntreFechasPorCategoriaConMontoIva"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_CON_SERIAL: nombreFileXML = "rpxFacturaEntreFechasConSerialPorArticulo"
      Case CM_RPT_FACTURAS_POR_TALONARIO: nombreFileXML = "rpxFacturasPorTalonario"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO: nombreFileXML = "rpxFacturaEntreFechasCamposDefContacto"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL: nombreFileXML = "rpxFacturaEntreFechasCamposDefMontoOriginal"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO_X_VENDEDOR: nombreFileXML = "rpxFacturaEntreFechasCamposDefContactoXVendedor"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL_X_VENDEDOR: nombreFileXML = "rpxFacturaEntreFechasCamposDefMontoOriginalXVendedor"
      Case CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_CLIENTE: nombreFileXML = "rpxFacturacionEntreFechasContactoXCliente"
      Case CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_CLIENTE: nombreFileXML = "rpxFacturacionEntreFechasMontoOriginalXCliente"
      Case CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN: nombreFileXML = "rpxFacturacionPorArticuloYPorAlmacen"
      Case CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN_RESUMIDO: nombreFileXML = "rpxFacturacionPorArticuloYPorAlmacenResumido"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeFactura = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeFactura(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeFactura = ""
   End If
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreFileDeImpresionDelReporteDeResumenDiario(ByVal valReporte As Integer, _
               Optional ByVal valTalonario As enum_Talonario = enum_Talonario.eTa_TALONARIO1) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS: nombreFileXML = "rpxResumenDiarioEntreFechas"
      Case CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS_POR_MAQUINA_FISCAL: nombreFileXML = "rpxResumenDiarioEntreFechasPorMaquinaFiscal"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeResumenDiario = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeFactura(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeResumenDiario = ""
   End If
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeResumenDiario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeResumenDiario(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS: fNombreDelReporteDeResumenDiario = "Resumen Diario Entre Fechas"
      Case CM_RPT_RESUMEN_DIARIO_ENTRE_FECHAS_POR_MAQUINA_FISCAL: fNombreDelReporteDeResumenDiario = " Resumen Diario Entre Fechas Por Maquina Fiscal"
      Case Else: fNombreDelReporteDeResumenDiario = ""
   End Select
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
           "fNombreDelReporteDeResumenDiario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeFactura(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_INFORME_DE_NOTAS_DE_CREDITO_ASOCIADAS_A_FACTURAS: fNombreDelReporteDeFactura = "Informe de Notas de Credito Asociadas a Facturas"
      Case CM_RPT_INFORME_DE_FACTURACION_CON_OTROS_CARGOS_Y_DESCUENTOS: fNombreDelReporteDeFactura = "Informe de Facturación Con Otros Cargos y Descuentos"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_X_CONDICION_PAGO: fNombreDelReporteDeFactura = "Listado de Facturación por Condición de Pago"
      Case CM_RPT_DIRECCIONES_DE_DESPACHO_X_FACTURA: fNombreDelReporteDeFactura = "Direcciones de Despacho por Factura"
      Case CM_RPT_NOTAS_DE_CREDITO_ENTRE_FECHAS_POR_VENDEDOR: fNombreDelReporteDeFactura = "Notas de Crédito entre Fechas Por Vendedor"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_POR_ARTICULO: fNombreDelReporteDeFactura = "Factura entre Fechas con Costos por Artículo"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_SEGUN_LEY_POR_ARTICULO: fNombreDelReporteDeFactura = "Factura Entre Fechas Con Costos Según Ley por Artículo"
      Case CM_RPT_SERVICIOS_POR_VEHICULO: fNombreDelReporteDeFactura = "Servicios por Vehículo"
      Case CM_RPT_SERVICIOS_DE_VEHICULOS_POR_CATEGORIA: fNombreDelReporteDeFactura = "Servicios a Vehículos Por Categoría"
      Case CM_RPT_HORAS_POR_SERVICIO: fNombreDelReporteDeFactura = "Horas de Servicios por Mecánico"
      Case CM_RPT_COMISION_DE_SERVICIO: fNombreDelReporteDeFactura = "Comisión de Servicios por Mecánico"
      Case CM_RPT_LISTADO_DE_VEHICULOS: fNombreDelReporteDeFactura = "Listado de Vehículos por Propietario"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA: fNombreDelReporteDeFactura = "Facturación entre Fechas por Categoría"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MUESTRA_MONTO: fNombreDelReporteDeFactura = "Facturación entre Fechas por Categoría"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_LINEA: fNombreDelReporteDeFactura = "Facturación entre Fechas por Linea De Producto"
      Case CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MONTO_IVA: fNombreDelReporteDeFactura = "Facturación entre Fechas por Categoría"
      Case CM_RPT_FACTURAS_ENTRE_FECHAS_CON_SERIAL: fNombreDelReporteDeFactura = "Articulos Facturados con Serial"
      Case CM_RPT_FACTURAS_POR_TALONARIO: fNombreDelReporteDeFactura = "Facturas Por Talonario"
      Case Else: fNombreDelReporteDeFactura = ""
   End Select
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
           "fNombreDelReporteDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturacionPorCondicionesDePago(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                  ByVal valSqlDelReporte As String, _
                                                                   ByVal valfechaInicio As Date, _
                                                                    ByVal valFechaFinal As Date, _
                                                                     ByVal valCompaniaParaInforme As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_X_CONDICION_PAGO)
   NombreDelReporte = fNombreDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_X_CONDICION_PAGO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCondicionPago", "", "CondicionesDePago"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaDelReporte"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeDocumento", "", "TipoDeDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "TotalFactura"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturacionPorCondicionesDePago = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:  Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
          "fConfigurarDatosDelReporteFacturacionPorCondicionesDePago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteNotasDeCreditoAsociadasAFacturas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                               ByVal valSqlDelReporte As String, _
                                                                ByVal valfechaInicio As Date, _
                                                                 ByVal valFechaFinal As Date, _
                                                                  ByVal valUsaCambio As Boolean, _
                                                                   ByVal valCompaniaParaInforme As String, _
                                                                    ByVal gGlobalization As Object, _
                                                                     ByVal valNombreMonedaLocal As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_INFORME_DE_NOTAS_DE_CREDITO_ASOCIADAS_A_FACTURAS)
   NombreDelReporte = fNombreDelReporteDeFactura(CM_RPT_INFORME_DE_NOTAS_DE_CREDITO_ASOCIADAS_A_FACTURAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblIva", gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", valNombreMonedaLocal, ""
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFactura", "", "NumeroFacturaAfectada"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaFactura", "", "Fecha"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalFactura", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusFactura", "", "factura_1.StatusFactura"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaNotaCredito", "", "FechaNC", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroNotaCredito", "", "NumeroNC"
'         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "TotalBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIVA", "", "TotalIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalNotaCredito", "", "TotalNotadeCredito"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusNotaCredito", "", "StatusNC"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteNotasDeCreditoAsociadasAFacturas = exito
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarDatosDelReporteNotasDeCreditoAsociadasAFacturas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturacionConOtrosCargosYDescuentos(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                               ByVal valSqlDelReporte As String, _
                                                                ByVal valfechaInicio As Date, _
                                                                 ByVal valFechaFinal As Date, _
                                                                  ByVal valUsaCambio As Boolean, _
                                                                   ByVal valCompaniaParaInforme As String, _
                                                                    ByVal valNombreMonedaLocal As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_INFORME_DE_FACTURACION_CON_OTROS_CARGOS_Y_DESCUENTOS)
   NombreDelReporte = fNombreDelReporteDeFactura(CM_RPT_INFORME_DE_FACTURACION_CON_OTROS_CARGOS_Y_DESCUENTOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", valNombreMonedaLocal, ""
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFactura", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaFactura", "", "Fecha"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalFactura", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglonOtrosCargos", "", "TotalRenglonOtrosCargos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescripcionOtrosCargos", "", "DescripcionOtrosCargos"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Nombre"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturacionConOtrosCargosYDescuentos = exito
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfigurarDatosDelReporteFacturacionConOtrosCargosYDescuentos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturacionPorArticuloYPorVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                  ByVal valSqlDelReporte As String, _
                                                                   ByVal valfechaInicio As Date, _
                                                                    ByVal valFechaFinal As Date, _
                                                                     ByVal valDetallado As Boolean, _
                                                                      ByVal cmbCantidadAImprimirArticulo As String, _
                                                                       ByVal gGlobalization As Object, _
                                                                        ByVal valCompaniaParaInforme As String, _
                                                                         ByVal valCompaniaUsaPrecioSinIvaSegunTipoDocumento As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   If valDetallado Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_DETALLADO)

      NombreDelReporte = "Facturación por Artículo por Vendedor - Detallado"
   Else
      If cmbCantidadAImprimirArticulo = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO_ARTICULO)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_VENDEDOR_RESUMIDO)
      End If
      NombreDelReporte = "Facturación por Artículo por Vendedor - Resumido"
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblArticulo", "Artículo"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "CodigoVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
         If valDetallado Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioUnitario", "", "PrecioUnitario", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoCompra", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMargen", "", "MargenGanancia"
         End If
                 
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNota", "Nota: Los Precios presentados No Incluyen el IVA."
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNota", "Nota: Los Precios presentados Incluyen el IVA."
         End If
         If valDetallado Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddODefault, NombreDelReporte
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         End If
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      Else
         exito = False
      End If
   Else
      exito = False
   End If
   fConfigurarDatosDelReporteFacturacionPorArticuloYPorVendedor = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturacionPorArticuloYPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturacionPorArticuloYPorAlmacen(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                            ByVal valSqlDelReporte As String, _
                                            ByVal valfechaInicio As Date, _
                                            ByVal valFechaFinal As Date, _
                                            ByVal cmbCantidadAImprimirArticulo As String, _
                                            ByVal gGlobalization As Object, _
                                            ByVal valCompaniaParaInforme As String, _
                                            ByVal valDetallado As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
  
    If valDetallado Then
        nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN)
        NombreDelReporte = "Salida de Invetario Por Almacen y Por Articulo - Detallado"
   Else
        nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURACION_POR_ARTICULO_Y_POR_ALMACEN_RESUMIDO)
        NombreDelReporte = "Salida de Invetario Por Almacen y Por Articulo - Resumido"
   End If

   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblArticulo", "Artículo"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
        "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtExistencia", "", "Existencia"
            
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lbFechaEmision", "Al " & gConvert.dateToString(gUtilDate.getFechaDeHoy)
        
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
        
        If valDetallado Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "FacturaNumero"
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddODefault, NombreDelReporte
        Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
        End If
       
        Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
        Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
        exito = True
        
      Else
        exito = False
      End If
   Else
      exito = False
   End If
   fConfigurarDatosDelReporteFacturacionPorArticuloYPorAlmacen = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturacionPorArticuloYPorAlmacen", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



Public Function fConfigurarDatosDelReporteFacturasPorTipoDeArticulo(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, _
                                                          ByVal valfechaInicio As Date, _
                                                           ByVal valFechaFinal As Date, _
                                                            ByVal valTituloDelReporte As String, _
                                                             ByVal valUsaCambio As Boolean, _
                                                              ByVal valMostrarDetalle As Boolean, _
                                                               ByVal valCompaniaParaInforme As String, _
                                                                ByVal gGlobalization As Object, _
                                                                 ByVal valUsaModuloDeContabilidad As Boolean, _
                                                                  ByVal valUsaPrecioSinIvaSegunTipoDocumento As Boolean, _
                                                                   ByVal valMuestraMonto As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   If valMuestraMonto Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MUESTRA_MONTO)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_POR_TIPO_ARTICULO_MONTO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusFactura", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "Codigo"
         If Not valUsaModuloDeContabilidad Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         If valUsaPrecioSinIvaSegunTipoDocumento Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIVA", False, ddTXRight, formatoNDecimales
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioConIVA", False, ddTXRight, formatoNDecimales
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "PorcDectoRenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuentoFactura", "", "PorcDectoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
                 
         If valMuestraMonto Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescItem", "", "MontoDescRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescFact", "", "MontoDescFactura"
         End If
         If valMostrarDetalle Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesRenglon", "TotalRenglon", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotRenglon", "TotalRenglon", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesMonto", "TotalRenglon", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesRenglon", "", "TotalRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotRenglon", "", "TotalRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesMonto", "", "TotalRenglon"
         End If

         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "ChkDetalle", valMostrarDetalle
         If valMuestraMonto Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         End If
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturasPorTipoDeArticulo = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDeDireccionesDeDespachoXFactura(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                 ByVal valSqlDelReporte As String, _
                                                                  ByVal valFechaInicial As Date, _
                                                                   ByVal valFechaFinal As Date, _
                                                                    ByVal NombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_DIRECCIONES_DE_DESPACHO_X_FACTURA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                    gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeDocumento", "", "TipoDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonoCliente", "", "TelefonoFiscal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "PersonaContactoDespacho"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionDeDespacho", "", "DireccionDespacho"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", "CiudadDespacho"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtZonaPostal", "", "ZonaPostalDespacho"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipoDocumento", "TipoDocumento", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_DIRECCIONES_DE_DESPACHO_X_FACTURA)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDeDireccionesDeDespachoXFactura = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDeDireccionesDeDespachoXFactura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturasOBorradoresEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal valTituloDelReporte As String, ByVal valVerContacto As Boolean, ByVal valUsaCambio As Boolean, _
                  ByVal valAgrupadoXVendedor As Boolean, ByVal NombreCompaniaParaInformes As String, ByVal gGlobalization As Object, _
                  ByVal valUsaModuloDeContabilidad As Boolean, ByVal valEmiteCobroDirecto As Boolean, ByVal valAgrupadoXCliente As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valVerContacto Then
      If valAgrupadoXVendedor Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_VENDEDOR)
      ElseIf valAgrupadoXCliente Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO_X_CLIENTE)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_CONTACTO)
      End If
   Else
      If valAgrupadoXVendedor Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_VENDEDOR)
      ElseIf valAgrupadoXCliente Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL_X_CLIENTE)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_O_BORRADORES_ENTRE_FECHAS_MONTO_ORIGINAL)
      End If
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, , 180) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIva", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumento", "", "TipoDeDocumentoStr"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "TotalBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "TotalIVA"
'nelson
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "TotalMontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIVA", "", ""
'nelson
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoExento", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalTotal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtrosCargos", "", "OtrosCargos"
         If Not valUsaModuloDeContabilidad Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If

         If valEmiteCobroDirecto Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComprobanteFiscal", "", "ComprobanteFiscal"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComprobanteFiscal", "", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroTarjeton", "", "NumeroTarjetonServicio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "TotalRenglones"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFormaDePago", "", "FormaDePago"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAsociado", "", "CodigoAsociado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalArticulo", "", "TotalArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglones", "", "TotalRenglones"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDescuesto", "TotalDescuento"
         If valVerContacto Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         End If
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone ' ddNPAfter
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipoDocumento", "TipoDeDocumentoStr", ddGrpAll, ddRepeatAll, True, ddNPNone
         If valAgrupadoXVendedor Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "Vendedor"
            
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "Vendedor", ddGrpAll, ddRepeatAll, True, ddNPBefore ', ddNPNone
            
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalVendedorBaseImponible", "TotalBaseImponible", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalVendedorMontoIva", "TotalIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalVendedorTotal", "TotalFactura", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         End If
         If valAgrupadoXCliente Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreClienteG", "", "Nombre"
            
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "Nombre", ddGrpAll, ddRepeatAll, True, ddNPNone ', ddNPNone
            
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalClienteBaseImponible", "TotalBaseImponible", eSF_SUM, "GHCliente", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalClienteMontoIva", "TotalIVA", eSF_SUM, "GHCliente", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalClienteTotal", "TotalFactura", eSF_SUM, "GHCliente", eSR_GROUP, eST_SUB_TOTAL
         End If
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosBaseImponible", "TotalBaseImponible", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosMontoIva", "TotalIVA", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosTotal", "TotalFactura", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosMontoExento", "TotalMontoExento", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "TotalBaseImponible", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIva", "TotalIVA", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoExento", "TotalMontoExento", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotal", "TotalFactura", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         
         
         If Not valVerContacto Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosOtrosCargos", "OtrosCargos", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalOtrosCargos", "OtrosCargos", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         End If

      End If
   End If
   fConfigurarDatosDelReporteFacturasOBorradoresEntreFechas = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasOBorradoresEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturacionXDia(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                     ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valReporteEnMonedaLocal As Boolean, _
                     ByVal NombreCompaniaParaInformes As String, ByVal gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_X_DIA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Resumen de Facturación por Dia"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         If valReporteEnMonedaLocal Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioDelDia)
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", "Cambio"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaOriginal", "Moneda"
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMensajeDelCambioDeLaMoneda", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaOriginal", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMensajeDelCambioDeLaMoneda", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaOriginal", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMensajeDelCambioDeLaMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaOriginal", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "MonedaDelReporte"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDiaDeLaSemana", "", "DiaDeLaSemanaStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaOriginal", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioABolivares"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFacturado", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDeFacturasAlDia", "", "ContadorFacturas"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMayorFactura", "", "FacturaMayor"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaFacturas", "Nota: los montos expresados de las Facturas son sin " & gGlobalization.fPromptIVA
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "MonedaDelReporte", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoFacturado", "MontoTotal", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCantidadDeFacturasAlDia", "ContadorFacturas", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
           
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, "Resumen de Facturación por día"
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteFacturacionXDia = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fConfigurarDatosDelReporteFacturacionXDia", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteNotasDeCreditoEntreFechasPorVendedor(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal valTituloDelReporte As String, ByVal valUsaCambio As Boolean, _
                  ByVal NombreCompaniaParaInformes As String, ByVal gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_NOTAS_DE_CREDITO_ENTRE_FECHAS_POR_VENDEDOR)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtMoneda", "", "Moneda", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVendedor", "", "NombreVendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeDocumentoStr", "", "TipoDeDocumentoStr"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Cliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComprobante", "", "Comprobante"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcDescuento", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDescuento", "", "MontoDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtrosCargos", "", "OtrosCargos"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoOriginalMoneda_SecMoneda", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalPorcDescuentoMoneda_SecMoneda", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoDescuentoMoneda_SecMoneda", "", "MontoDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalBaseImponibleMoneda_SecMoneda", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoExentoMoneda_SecMoneda", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoIVAMoneda_SecMoneda", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoTotalMoneda_SecMoneda", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalOtrosCargosMoneda_SecMoneda", "", "OtrosCargos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesMoneda", "", "MontoTotal"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoOriginalVendedor_SecVendedor", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalPorcDescuentoVendedor_SecVendedor", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoDescuentoVendedor_SecVendedor", "", "MontoDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalBaseImponibleVendedor_SecVendedor", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoExentoVendedor_SecVendedor", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoIVAVendedor_SecVendedor", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoTotalVendedor_SecVendedor", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalOtrosCargosVendedor_SecVendedor", "", "OtrosCargos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesVendedor", "", "MontoTotal"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoOriginalTipoDocumento_SecTipoDocumento", "", "MontoOriginal "
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalPorcDescuentoTipoDocumento_SecTipoDocumento", "", "PorcentajeDescuento "
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoDescuentoTipoDocumento_SecTipoDocumento", "", "MontoDescuento "
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalBaseImponibleTipoDocumento_SecTipoDocumento", "", "BaseImponible "
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoExentoTipoDocumento_SecTipoDocumento", "", "MontoExento "
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoIVATipoDocumento_SecTipoDocumento", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalMontoTotalTipoDocumento_SecTipoDocumento", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalOtrosCargosTipoDocumento_SecTipoDocumento", "", "OtrosCargos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesTipodeDocumento", "", "MontoTotal"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTipoDocumento", "TipoDeDocumentoStr", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecVendedor", "NombreVendedor", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoOriginalMoneda_SecMoneda", "MontoOriginal", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalPorcDescuentoMoneda_SecMoneda", "PorcentajeDescuento", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoDescuentoMoneda_SecMoneda", "MontoDescuento", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalBaseImponibleMoneda_SecMoneda", "BaseImponible", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoExentoMoneda_SecMoneda", "MontoExento", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoIVAMoneda_SecMoneda", "MontoIVA", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoTotalMoneda_SecMoneda", "MontoTotal", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalOtrosCargosMoneda_SecMoneda", "OtrosCargos", eSF_SUM, "GHSecMoneda", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoOriginalVendedor_SecVendedor", "MontoOriginal", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalPorcDescuentoVendedor_SecVendedor", "PorcentajeDescuento", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoDescuentoVendedor_SecVendedor", "MontoDescuento", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalBaseImponibleVendedor_SecVendedor", "BaseImponible", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoExentoVendedor_SecVendedor", "MontoExento", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoIVAVendedor_SecVendedor", "MontoIVA", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoTotalVendedor_SecVendedor", "MontoTotal", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalOtrosCargosVendedor_SecVendedor", "OtrosCargos", eSF_SUM, "GHSecVendedor", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoOriginalTipoDocumento_SecTipoDocumento", "MontoOriginal", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalPorcDescuentoTipoDocumento_SecTipoDocumento", "PorcentajeDescuento", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoDescuentoTipoDocumento_SecTipoDocumento", "MontoDescuento", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalBaseImponibleTipoDocumento_SecTipoDocumento", "BaseImponible", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoExentoTipoDocumento_SecTipoDocumento", "MontoExento", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoIVATipoDocumento_SecTipoDocumento", "MontoIVA", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalMontoTotalTipoDocumento_SecTipoDocumento", "MontoTotal", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalOtrosCargosTipoDocumento_SecTipoDocumento", "OtrosCargos", eSF_SUM, "GHSecTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
               
         If valUsaCambio Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtT_TotalMontoOriginalMoneda_SecMoneda", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtT_TotalMontoOriginalVendedor_SecVendedor", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtT_TotalMontoOriginalTipoDocumento_SecTipoDocumento", False
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeFactura(CM_RPT_NOTAS_DE_CREDITO_ENTRE_FECHAS_POR_VENDEDOR)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteNotasDeCreditoEntreFechasPorVendedor = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteNotasDeCreditoEntreFechasPorVendedor = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteNotasDeCreditoEntreFechasPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturaEntreFechasConCostosSegunLeyPorArticulo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valMaxGanancia As Currency) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_SEGUN_LEY_POR_ARTICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
       
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Cliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVendedor", "", "Vendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtArticulo", "", "Articulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioVenta", "", "PrecioVenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioCosto", "", "PrecioCosto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVentaTotal", "", "VentaTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoTotal", "", "CostoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeGastoAdmisible", "", "PorcentajeGastoAdmisible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGastoAdmisible", "", "GastoAdmisible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoSegunLey", "", "CostoSegunLey"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGanancia", "", "PorcentajeGanancia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMayorAMaxGanancia", "", "MayorAMaxGanancia"
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_VentaTotal_SecFactura", "VentaTotal", eSF_SUM, "GHSecFactura", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecFactura", "CostoTotal", eSF_SUM, "GHSecFactura", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_GastoAdmisible_SecFactura", "GastoAdmisible", eSF_SUM, "GHSecFactura", eSR_ALL, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_VentaTotal_SecFactura", "", "VentaTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecFactura", "", "CostoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_GastoAdmisible_SecFactura", "", "GastoAdmisible"

         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_VentaTotal_SecTotalGeneral", "VentaTotal", eSF_SUM, "GHSecTotalGeneral", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "CostoTotal", eSF_SUM, "GHSecTotalGeneral", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_GastoAdmisible_SecTotalGeneral", "GastoAdmisible", eSF_SUM, "GHSecTotalGeneral", eSR_ALL, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_VentaTotal_SecTotalGeneral", "", "VentaTotal", False, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "", "CostoTotal", False, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_GastoAdmisible_SecTotalGeneral", "", "GastoAdmisible", False, ddTXRight
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMaxGanancia", valMaxGanancia
     
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpAll, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecFactura", "Numero", ddGrpAll, ddRepeatOnPage, False, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_SEGUN_LEY_POR_ARTICULO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturaEntreFechasConCostosSegunLeyPorArticulo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteFacturaEntreFechasConCostosSegunLeyPorArticulo = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturaEntreFechasConCostosPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturaEntreFechasConCostosPorArticulo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_POR_ARTICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
       
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Cliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVendedor", "", "Vendedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtArticulo", "", "Articulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioVenta", "", "PrecioVenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioCosto", "", "PrecioCosto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVentaTotal", "", "VentaTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoTotal", "", "CostoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferencia", "", "Diferencia"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_VentaTotal_SecFactura", "VentaTotal", eSF_SUM, "GHSecFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecFactura", "CostoTotal", eSF_SUM, "GHSecFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Diferencia_SecFactura", "Diferencia", eSF_SUM, "GHSecFactura", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_VentaTotal_SecFactura", "", "VentaTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecFactura", "", "CostoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Diferencia_SecFactura", "", "Diferencia"

         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_VentaTotal_SecTotalGeneral", "VentaTotal", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "CostoTotal", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Diferencia_SecTotalGeneral", "Diferencia", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_VentaTotal_SecTotalGeneral", "", "VentaTotal", False, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "", "CostoTotal", False, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Diferencia_SecTotalGeneral", "", "Diferencia", False, ddTXRight
     

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecFactura", "Numero", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_CON_COSTOS_POR_ARTICULO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturaEntreFechasConCostosPorArticulo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteFacturaEntreFechasConCostosPorArticulo = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturaEntreFechasConCostosPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteServiciosXVehiculo(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal valTituloDelReporte As String, ByVal NombreCompaniaParaInformes As String, ByVal gProyParametrosCompania As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_SERVICIOS_POR_VEHICULO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPlaca", "", "Placa"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtModelo", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtColor", "", "DescripcionColor"
         
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroFactura", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtKilometraje", "", "Kilometraje"
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtArticulo", "", "Categoria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcionrenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotal", "", "SuBTotal"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVehiculo", "Consecutivo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
                           
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalxVehiculo", "SuBTotal", eSF_SUM, "GHVehiculo", eSR_GROUP, eST_SUB_TOTAL
                
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeFactura(CM_RPT_SERVICIOS_POR_VEHICULO)
         
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteServiciosXVehiculo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteServiciosXVehiculo = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteServiciosXVehiculo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteServiciosDeVehiculosXCategoria(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal valTituloDelReporte As String, ByVal NombreCompaniaParaInformes As String, ByVal gProyParametrosCompania As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_SERVICIOS_DE_VEHICULOS_POR_CATEGORIA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPlaca", "", "Placa"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtModelo", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtKilometraje", "", "Kilometraje"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCorreoElectronico", "", "Email"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaUltimoServicio", eFT_DATE

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeFactura(CM_RPT_SERVICIOS_DE_VEHICULOS_POR_CATEGORIA)
         
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteServiciosDeVehiculosXCategoria = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteServiciosDeVehiculosXCategoria = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteServiciosDeVehiculosXCategoria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteHorasDeServicio(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal NombreCompaniaParaInformes As String, ByVal gProyParametrosCompania As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_HORAS_POR_SERVICIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "Nombre"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroFactura", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionServicio", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtFactor", "", "Factor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadXFactor", "", "FactorxCantidad"
         
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMecanico", True, 450
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMecanico", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalxMecanico", "FactorxCantidad", eSF_SUM, "GHMecanico", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeFactura(CM_RPT_HORAS_POR_SERVICIO)
         
         
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteHorasDeServicio = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteHorasDeServicio = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteHorasDeServicio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteComisionDeServicios(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal NombreCompaniaParaInformes As String, ByVal gProyParametrosCompania As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_COMISION_DE_SERVICIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoVendedor", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "Nombre"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNroFactura", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionServicio", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotal", "", "SubTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotalConDescuento", "", "SubTotalConDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtComision", "", "Comision"
         
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHMecanico", True, 450
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMecanico", "Codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalFacturaXMecanico", "SubTotalConDescuento", eSF_SUM, "GHMecanico", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotaComisionxMecanico", "Comision", eSF_SUM, "GHMecanico", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeFactura(CM_RPT_COMISION_DE_SERVICIO)
         
         
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteComisionDeServicios = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteComisionDeServicios = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComisionDeServicios", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelListadoDeVehiculosPorPropietario(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal NombreCompaniaParaInformes As String, ByVal gProyParametrosCompania As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_LISTADO_DE_VEHICULOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         'gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroRIF", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPlaca", "", "Placa"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerialVIN", "", "serialVIN"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtModelo", "", "NombreModelo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAno", "", "Ano"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtColor", "", "DescripcionColor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMarca", "", "Marca"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPoliza", "", "NumeroPoliza"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerialMotor", "", "SerialMotor"
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHCliente", True, 675
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "NombreCliente", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidad", "Placa", enum_SummaryFunc.eSF_COUNT, "GHCliente", eSR_GROUP, eST_SUB_TOTAL
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeFactura(CM_RPT_LISTADO_DE_VEHICULOS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelListadoDeVehiculosPorPropietario = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelListadoDeVehiculosPorPropietario = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelListadoDeVehiculosPorPropietario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturasPorCategoria(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, _
                                                          ByVal valfechaInicio As Date, _
                                                           ByVal valFechaFinal As Date, _
                                                            ByVal valTituloDelReporte As String, _
                                                             ByVal valUsaCambio As Boolean, _
                                                              ByVal valMostrarDetalle As Boolean, _
                                                               ByVal valCompaniaParaInforme As String, _
                                                                ByVal gGlobalization As Object, _
                                                                 ByVal valUsaModuloDeContabilidad As Boolean, _
                                                                  ByVal valUsaPrecioSinIvaSegunTipoDocumento As Boolean, _
                                                                   ByVal valMuestraMonto As Boolean, _
                                                                    ByVal valCantidadCategoria, _
                                                                      ByVal valCategoria, _
                                                                        Optional ByVal valMuestraMontoIva As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
  If valMuestraMonto Then
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MUESTRA_MONTO)
  Else
      If valMuestraMontoIva Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA_MONTO_IVA)
      Else
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA)
      End If
  End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", fNombreDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_POR_CATEGORIA)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusFactura", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "Codigo"
         If Not valUsaModuloDeContabilidad Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         If valMuestraMontoIva Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIva", "", "MontoIva", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesIva", "", "MontoIva", False, ddTXRight, formatoNDecimales
         End If
         If valUsaPrecioSinIvaSegunTipoDocumento Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIVA", False, ddTXRight, formatoNDecimales
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioConIVA", False, ddTXRight, formatoNDecimales
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "PorcDectoRenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuentoFactura", "", "PorcDectoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
         If valMuestraMonto Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescItem", "", "MontoDescRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescFact", "", "MontoDescFactura"
         End If
         If valMostrarDetalle Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesRenglon", "TotalRenglon", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotRenglon", "TotalRenglon", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesMonto", "TotalRenglon", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
            If valMuestraMontoIva Then
               gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesMontoIva", "TotalRenglonConIva", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
               gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotRenglonIva", "TotalRenglonConIva", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            End If
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesRenglon", "", "TotalRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotRenglon", "", "TotalRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesMonto", "", "TotalRenglon"
            If valMuestraMontoIva Then
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglonConIvaResumido", "", "TotalRenglonConIva"
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesMontoIva", "", "TotalRenglonConIva"
            End If
         End If
        gUtilReports.sDefaultConfigurationForCheckBox refRpt, "ChkDetalle", valMostrarDetalle
         If valMuestraMonto Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Else
            If valMuestraMontoIva Then
               gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
            Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
            End If
         End If
           gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCategoria", valCategoria, "Categoria"
         If valMuestraMontoIva Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIVA", "", "MontoIva"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRenglonIva", "", "TotalRenglonIva"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglonConIva", "", "TotalRenglonConIva"
         End If
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
      exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturasPorCategoria = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasPorCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fConfigurarDatosDelReporteFacturasPorLinea(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, _
                                                          ByVal valfechaInicio As Date, _
                                                           ByVal valFechaFinal As Date, _
                                                               ByVal valCompaniaParaInforme As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   Dim i As Integer
   i = gUtilReports.fConvertStringToCurrency(gProyParametrosCompania.GetCantidadDeDecimalesStr)
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_POR_LINEA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", fNombreDelReporteDeFactura(CM_RPT_FACTURA_ENTRE_FECHAS_POR_LINEA)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalExento", "", "MontoExento", False, ddTXRight, "#,##0.00"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDescuento", "", "Descuento", False, ddTXRight, "#,##0.00"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", "BaseImponible", False, ddTXRight, "#,##0.00"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIva", "", "TotalIva", False, ddTXRight, "#,##0.00"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalFactura", "", "TotalFactura", False, ddTXRight, "#,##0.00"
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
                  Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
      exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturasPorLinea = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasPorCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturasConSerial(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, _
                                                          ByVal valfechaInicio As Date, _
                                                           ByVal valFechaFinal As Date, _
                                                            ByVal valTituloDelReporte As String, _
                                                             ByVal valUsaCambio As Boolean, _
                                                              ByVal valMostrarDetalle As Boolean, _
                                                               ByVal valCompaniaParaInforme As String, _
                                                                ByVal gGlobalization As Object, _
                                                                 ByVal valUsaModuloDeContabilidad As Boolean, _
                                                                  ByVal valUsaPrecioSinIvaSegunTipoDocumento As Boolean, _
                                                                   ByVal valMuestraMonto As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_CON_SERIAL)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusFactura", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "Codigo"
         If Not valUsaModuloDeContabilidad Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "CantidadDeArticulo", False, ddTXRight, formatoNDecimales
         If valUsaPrecioSinIvaSegunTipoDocumento Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIVA", False, ddTXRight, formatoNDecimales
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioConIVA", False, ddTXRight, formatoNDecimales
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "PorcDectoRenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuentoFactura", "", "PorcDectoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
                 
         If valMuestraMonto Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescItem", "", "MontoDescRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescFact", "", "MontoDescFactura"
         End If
         If valMostrarDetalle Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesRenglon", "TotalRenglon", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotRenglon", "TotalRenglon", eSF_SUM, "GHFactura", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalesMonto", "TotalRenglon", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesRenglon", "", "TotalRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotRenglon", "", "TotalRenglon"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalesMonto", "", "TotalRenglon"
         End If

         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "ChkDetalle", valMostrarDetalle
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturasConSerial = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturasPorTalonario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, _
                                                           ByVal valFechaFinal As Date, _
                                                               ByVal valCompaniaParaInforme As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_POR_TALONARIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valCompaniaParaInforme
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTalonario", "", "Talonario"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuento", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalFactura", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusFactura", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTalonario", "TotalFactura", eSF_SUM, "GHSecFactura", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalFactura_SecFactura", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTalonario", "Talonario", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecFecha", "Fecha", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeFactura(CM_RPT_FACTURAS_POR_TALONARIO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturasPorTalonario = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteFacturasPorTalonario = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasPorTalonario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFacturasEntreFechasPorCamposDefinibles(ByRef refRpt As DDActiveReports2.ActiveReport, _
                  ByVal valSqlDelReporte As String, ByVal valfechaInicio As Date, ByVal valFechaFinal As Date, _
                  ByVal valTituloDelReporte As String, ByVal valVerContacto As Boolean, ByVal valUsaCambio As Boolean, _
                  ByVal valAgrupadoXVendedor As Boolean, ByVal NombreCompaniaParaInformes As String, ByVal gGlobalization As Object, _
                  ByVal valUsaModuloDeContabilidad As Boolean, ByVal valEmiteCobroDirecto As Boolean, _
                  ByVal valCampoDefinible As String, valFiltraPorCampoDefinible As Boolean, _
                  ByVal valCampoDefinibleFiltrado As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valVerContacto Then
      If valAgrupadoXVendedor Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO_X_VENDEDOR)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_CONTACTO)
      End If
   Else
      If valAgrupadoXVendedor Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL_X_VENDEDOR)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeFactura(CM_RPT_FACTURAS_ENTRE_FECHAS_X_CAMPOS_DEFINIBLES_MONTO_ORIGINAL)
      End If
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, , 180) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", NombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valfechaInicio) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMontoIva", "Monto " & gGlobalization.fPromptIVA
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaCambio Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", gEnumReport.getMensajesDeMonedaParaInformes(eMM_CambioOriginal)
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", True
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumento", "", "TipoDeDocumentoStr"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "TotalBaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "TotalIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "TotalMontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "TotalFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoOriginal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoIVA", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoExento", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalTotal", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtrosCargos", "", "OtrosCargos"
         If Not valUsaModuloDeContabilidad Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lbNComprobante", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNComprobante", False
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNComprobante", "", "NumeroComprobante"
         End If

         If valEmiteCobroDirecto Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComprobanteFiscal", "", "ComprobanteFiscal"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComprobanteFiscal", "", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroTarjeton", "", "NumeroTarjetonServicio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "TotalRenglones"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDescuento", "", "Descuento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFormaDePago", "", "FormaDePago"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAsociado", "", "CodigoAsociado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalArticulo", "", "TotalArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalRenglones", "", "TotalRenglones"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDescuesto", "TotalDescuento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible", "", "CampoDefinible"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinible", valCampoDefinible
         If valFiltraPorCampoDefinible And valCampoDefinibleFiltrado <> "" Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblFiltradoPor", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCampoDefinibleFiltrado", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinibleFiltrado", valCampoDefinibleFiltrado
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFiltradoPor", valCampoDefinible
         End If

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPBeforeAfter ' ddNPAfter
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipoDocumento", "TipoDeDocumentoStr", ddGrpAll, ddRepeatAll, True, ddNPNone
         If valAgrupadoXVendedor Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreVendedor", "", "Vendedor"
            
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHVendedor", "Vendedor", ddGrpAll, ddRepeatAll, True, ddNPBefore ', ddNPNone
            
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalVendedorBaseImponible", "TotalBaseImponible", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalVendedorMontoIva", "TotalIVA", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalVendedorTotal", "TotalFactura", eSF_SUM, "GHVendedor", eSR_GROUP, eST_SUB_TOTAL
         End If
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosBaseImponible", "TotalBaseImponible", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosMontoIva", "TotalIVA", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosTotal", "TotalFactura", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosMontoExento", "TotalMontoExento", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "TotalBaseImponible", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIva", "TotalIVA", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoExento", "TotalMontoExento", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTotal", "TotalFactura", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         
         
         If Not valVerContacto Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentosOtrosCargos", "OtrosCargos", eSF_SUM, "GHTipoDocumento", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalOtrosCargos", "OtrosCargos", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteFacturasEntreFechasPorCamposDefinibles = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFacturasEntreFechasPorCamposDefinibles", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


