VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsArticuloInvRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
 
Private Const CM_FILE_NAME As String = "clsArticuloInvRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Articulo Iventario"

Private Enum eArtInvRpt
   eAIRpt_LISTADO_DE_ARTICULOS = 0
   eAIRpt_ARTICULO_DE_INVENTARIO_RESUMIDO
   eAIRpt_ARTICULO_DE_INVENTARIO_DETALLADO
   eAIRpt_MOVIMIENTOS_INVENTARIO
   eAIRpt_VENCIMIENTO_DE_PRODUCTOS
   eAIRpt_GANANCIA_POR_PRODUCTO
   eAIRpt_LISTA_DE_PRECIOS
   eAIRpt_HOJA_DE_TRABAJO_DEL_PEDIDO
   eAIRpt_PRODUCTOS_POR_DEBAJO_EXIST_MINIMA
   eAIRpt_ARTICULOS_SIN_VENTA
   eAIRpt_EXISTENCIA_POR_ARTICULO
   eAIRpt_EXISTENCIA_POR_ALMACEN
   eAIRpt_COSTO_POR_ALMACEN
   eAIRpt_NOTA_ES_ENTRE_FECHAS
   eAIRpt_TRANSFERENCIAS_ENTRE_FECHAS
   eAIRpt_FORMATO_DE_CONTEO_FISICO
   eAIRpt_CONTEO_FISICO
   eAIRpt_CONTEO_FISICO_ENTRE_FECHAS
   eAIRpt_TRANSFERENCIA
   eAIRpt_REGSITRO_DETALLADO
   eAIRpt_EXISTENCIA_ENTRE_FECHAS
   eAIRpt_VALORACION_DE_INVENTARIO
   eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA
   eAIRpt_VALORACION_DE_INVENTARIO_ME
   eAIRpt_LISTADO_ARTICULO_TALLA_COLOR
   eAIRpt_LISTADO_ARTICULOS_CON_RESERVA_POR_FACTURACION
   eAIRpt_LISTADO_ARTICULOS_POR_RETIRAR_ENTRE_FECHAS
   eAIRpt_IMPRESION_DE_COD_DE_BARRAS
   eAIRpt_CONSULTAR_CORTES
   eAIRpt_LISTA_DE_PRECIOS_EN_BSF
   eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_EN_BSF
   eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONONIBILIDAD
   eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_EN_UNIDADES_FISICAS
   eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_VALORIZADO
   eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_VALORIZADO_PERU
   eAIRpt_DISPONIBILIDAD_DE_INVENTARIO
   eAIRpt_DISPONIBILIDAD_DE_INVENTARIO_EN_TRANSITO
   eAIRpt_EXISTENCIA_DETALLADA_POR_ARTICULO
   eAIRpt_EXISTENCIA_MERCANCIA_EN_TRANSITO
   eAIRpt_COSTO_ULTIMA_COMPRA
   eAIRpt_LISTADO_ARTICULOSX_ALMACENX_TIPO
   eAIRpt_VALORACION_DEL_INVENTARIO_POR_ALMACEN
   eAIRpt_COSTO_PROMEDIO_GENERAL
   eAIRpt_COSTOS_ARTICULOS_COMPUESTOS
   eAIRpt_INVENTARIO_POR_CATEGORIA
   eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_CATEGORIA
   eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_LINEA_DE_PRODUCTO
   eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO
   eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO_POR_ALMACEN
   eAIRpt_GANANCIA_POR_PRODUCTO_SEGUN_LEY_COSTO
   eAIRpt_LISTA_DE_PRECIOS_CON_MARCA
   eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_MARCA
   eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONIBILIDAD_Y_MARCA
   
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDeArticuloDeInventario(ByVal valReporte As eArtInvRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eAIRpt_LISTADO_DE_ARTICULOS: nombreFileXML = "rpxListadoDeArticulos"
      Case eAIRpt_ARTICULO_DE_INVENTARIO_RESUMIDO: nombreFileXML = "rpxArticulosDeInventarioResumido"
      Case eAIRpt_ARTICULO_DE_INVENTARIO_DETALLADO: nombreFileXML = "rpxArticulosDeInventarioDetallado"
      Case eAIRpt_MOVIMIENTOS_INVENTARIO: nombreFileXML = "rpxMovimientosDeInventario"
      Case eAIRpt_VENCIMIENTO_DE_PRODUCTOS: nombreFileXML = "rpxVencimientoDeProductos"
      Case eAIRpt_GANANCIA_POR_PRODUCTO: nombreFileXML = "rpxGananciaPorProducto"
      Case eAIRpt_LISTA_DE_PRECIOS: nombreFileXML = "rpxListaDePrecios"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA: nombreFileXML = "rpxListaDePreciosConExistencia"
      Case eAIRpt_HOJA_DE_TRABAJO_DEL_PEDIDO: nombreFileXML = "rpxHojaDeTrabajoDePedido"
      Case eAIRpt_PRODUCTOS_POR_DEBAJO_EXIST_MINIMA: nombreFileXML = "rpxProductosPorDebajoDeLaExistenciaMinima"
      Case eAIRpt_ARTICULOS_SIN_VENTA: nombreFileXML = "rpxArticulosSinVenta"
      Case eAIRpt_EXISTENCIA_POR_ARTICULO: nombreFileXML = "rpxExistenciaPorArticulo"
      Case eAIRpt_EXISTENCIA_POR_ALMACEN: nombreFileXML = "rpxExistenciaPorAlmacen"
      Case eAIRpt_COSTO_POR_ALMACEN: nombreFileXML = "rpxCostoPorAlmacen"
      Case eAIRpt_NOTA_ES_ENTRE_FECHAS: nombreFileXML = "rpxNotasDeEntradaSalidaEntreFechas"
      Case eAIRpt_TRANSFERENCIAS_ENTRE_FECHAS: nombreFileXML = "rpxTransferenciasEntreFechas"
      Case eAIRpt_FORMATO_DE_CONTEO_FISICO: nombreFileXML = "rpxFormatoDeConteoFisico"
      Case eAIRpt_CONTEO_FISICO: nombreFileXML = "rpxConteoFisico"
      Case eAIRpt_CONTEO_FISICO_ENTRE_FECHAS: nombreFileXML = "rpxConteosFisicoEntreFechas"
      Case eAIRpt_TRANSFERENCIA: nombreFileXML = "rpxTransferencia"
      Case eAIRpt_REGSITRO_DETALLADO: nombreFileXML = "rpxRegistroDetalladoInventario"
      Case eAIRpt_EXISTENCIA_ENTRE_FECHAS: nombreFileXML = "rpxExistenciaDeInventarioEntreFechas"
      Case eAIRpt_VALORACION_DE_INVENTARIO: nombreFileXML = "rpxValoracionDeInventario"
      Case eAIRpt_VALORACION_DE_INVENTARIO_ME: nombreFileXML = "rpxValoracionDeInventarioME"
      Case eAIRpt_LISTADO_ARTICULO_TALLA_COLOR: nombreFileXML = "rpxListaDeExistenciaPorTallaYColor"
      Case eAIRpt_LISTADO_ARTICULOS_CON_RESERVA_POR_FACTURACION: nombreFileXML = "rpxArticulosConReservaPorFacturacion"
      Case eAIRpt_LISTADO_ARTICULOS_POR_RETIRAR_ENTRE_FECHAS: nombreFileXML = "rpxListadoArticulosPorRetirarEntreFechas"
      Case eAIRpt_IMPRESION_DE_COD_DE_BARRAS: nombreFileXML = "rpxImpresionDeCodigoDeBarras"
      Case eAIRpt_CONSULTAR_CORTES: nombreFileXML = "rpxConsultadeCortes"
      Case eAIRpt_LISTA_DE_PRECIOS_EN_BSF: nombreFileXML = "rpxListaDePreciosBsF"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_EN_BSF: nombreFileXML = "rpxListaDePreciosConExistenciaBsF"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONONIBILIDAD: nombreFileXML = "rpxListaDePreciosConExistenciaYReserva"
      Case eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_EN_UNIDADES_FISICAS: nombreFileXML = "rpxInventarioPermanenteFisico"
      Case eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_VALORIZADO: nombreFileXML = "rpxInventarioPermanenteValorizado"
      Case eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_VALORIZADO_PERU: nombreFileXML = "rpxInventarioPermanenteValorizadoPeru"
      Case eAIRpt_DISPONIBILIDAD_DE_INVENTARIO: nombreFileXML = "rpxDisponibilidadDeArticulos"
      Case eAIRpt_DISPONIBILIDAD_DE_INVENTARIO_EN_TRANSITO: nombreFileXML = "rpxDisponibilidadDeArticulosEnTransito"
      Case eAIRpt_EXISTENCIA_DETALLADA_POR_ARTICULO: nombreFileXML = "rpxExistenciaDetalladaXArticuloSerialRollo"
      Case eAIRpt_EXISTENCIA_MERCANCIA_EN_TRANSITO: nombreFileXML = "rpxExistenciaMercanciaEnTransito"
      Case eAIRpt_COSTO_ULTIMA_COMPRA: nombreFileXML = "rpxCostoPromedio"
      Case eAIRpt_LISTADO_ARTICULOSX_ALMACENX_TIPO: nombreFileXML = "rpxListadoArticulosxAlmacenxTipo"
      Case eAIRpt_VALORACION_DEL_INVENTARIO_POR_ALMACEN: nombreFileXML = "rpxCostoPromedioPorAlmacen"
      Case eAIRpt_COSTO_PROMEDIO_GENERAL: nombreFileXML = "rpxCostoPromedioGeneral"
      Case eAIRpt_COSTOS_ARTICULOS_COMPUESTOS: nombreFileXML = "rpxCostosArticulosCompuestos"
      Case eAIRpt_INVENTARIO_POR_CATEGORIA: nombreFileXML = "rpxInventarioPorCategoria"
      Case eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_CATEGORIA: nombreFileXML = "rpxInventarioPorCategoriaFiltradoPorCategoria"
      Case eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_LINEA_DE_PRODUCTO: nombreFileXML = "rpxInventarioPorCategoriaFiltradoPorLineaDeProducto"
      Case eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO: nombreFileXML = "rpxDisponibilidadArticuloInventario"
      Case eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO_POR_ALMACEN: nombreFileXML = "rpxDisponibilidadArticuloInventarioPorAlmacen"
      Case eAIRpt_GANANCIA_POR_PRODUCTO_SEGUN_LEY_COSTO: nombreFileXML = "rpxGananciaPorProductoSegunLeyCosto"
      Case eAIRpt_LISTA_DE_PRECIOS: nombreFileXML = "rpxListaDePrecios"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA: nombreFileXML = "rpxListaDePreciosConExistencia"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_MARCA: nombreFileXML = "rpxListaDePreciosConMarca"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_MARCA: nombreFileXML = "rpxListaDePreciosConExistenciaYMarca"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONIBILIDAD_Y_MARCA:    nombreFileXML = "rpxListaDePreciosConExistenciaYReservaYMarca"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeArticuloDeInventario = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fEnumReporteDeArticulosDeInventarioToString(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeArticuloDeInventario = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeArticuloDeInventario", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEnumReporteDeArticulosDeInventarioToString(ByVal valReporte As eArtInvRpt) As String
   Select Case valReporte
      Case eAIRpt_LISTADO_DE_ARTICULOS: fEnumReporteDeArticulosDeInventarioToString = "Listado de Artículos de Inventario"
      Case eAIRpt_ARTICULO_DE_INVENTARIO_RESUMIDO: fEnumReporteDeArticulosDeInventarioToString = "Artículos de Inventario - Resumido"
      Case eAIRpt_ARTICULO_DE_INVENTARIO_DETALLADO: fEnumReporteDeArticulosDeInventarioToString = "Artículos de Inventario - Detallado"
      Case eAIRpt_MOVIMIENTOS_INVENTARIO: fEnumReporteDeArticulosDeInventarioToString = "Movimientos de Inventario"
      Case eAIRpt_VENCIMIENTO_DE_PRODUCTOS: fEnumReporteDeArticulosDeInventarioToString = "Vencimiento de Productos"
      Case eAIRpt_GANANCIA_POR_PRODUCTO: fEnumReporteDeArticulosDeInventarioToString = "Ganancia por Producto"
      Case eAIRpt_LISTA_DE_PRECIOS: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios"
      Case eAIRpt_HOJA_DE_TRABAJO_DEL_PEDIDO: fEnumReporteDeArticulosDeInventarioToString = "Hoja de Trabajo de Pedido"
      Case eAIRpt_PRODUCTOS_POR_DEBAJO_EXIST_MINIMA: fEnumReporteDeArticulosDeInventarioToString = "Productos por Debajo de la Existencia Mínima"
      Case eAIRpt_ARTICULOS_SIN_VENTA: fEnumReporteDeArticulosDeInventarioToString = "Artículos Sin Venta"
      Case eAIRpt_EXISTENCIA_POR_ARTICULO: fEnumReporteDeArticulosDeInventarioToString = "Existencia por Articulo"
      Case eAIRpt_EXISTENCIA_POR_ALMACEN: fEnumReporteDeArticulosDeInventarioToString = "Existencia por Almacen"
      Case eAIRpt_COSTO_POR_ALMACEN: fEnumReporteDeArticulosDeInventarioToString = "Costo por Almacen"
      Case eAIRpt_NOTA_ES_ENTRE_FECHAS: fEnumReporteDeArticulosDeInventarioToString = "Notas de Entrada / Salida entre Fechas"
      Case eAIRpt_TRANSFERENCIAS_ENTRE_FECHAS: fEnumReporteDeArticulosDeInventarioToString = "Transferencias entre Fechas"
      Case eAIRpt_FORMATO_DE_CONTEO_FISICO: fEnumReporteDeArticulosDeInventarioToString = "Formato de Conteo Físico"
      Case eAIRpt_CONTEO_FISICO: fEnumReporteDeArticulosDeInventarioToString = "Conteo Físco"
      Case eAIRpt_CONTEO_FISICO_ENTRE_FECHAS: fEnumReporteDeArticulosDeInventarioToString = "Conteos Físico entre Fechas"
      Case eAIRpt_TRANSFERENCIA: fEnumReporteDeArticulosDeInventarioToString = "Transferencia"
      Case eAIRpt_EXISTENCIA_ENTRE_FECHAS: fEnumReporteDeArticulosDeInventarioToString = "Existencia entre Fechas"
      Case eAIRpt_REGSITRO_DETALLADO: fEnumReporteDeArticulosDeInventarioToString = "Registro Detallado"
      Case eAIRpt_VALORACION_DE_INVENTARIO: fEnumReporteDeArticulosDeInventarioToString = "Valoración de Inventario"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Existencia"
      Case eAIRpt_LISTADO_ARTICULOS_CON_RESERVA_POR_FACTURACION: fEnumReporteDeArticulosDeInventarioToString = "Listado de Articulos con Reserva en Facturas"
      Case eAIRpt_LISTADO_ARTICULOS_POR_RETIRAR_ENTRE_FECHAS: fEnumReporteDeArticulosDeInventarioToString = "Listado de Articulos por Retirar entre Fechas"
      Case eAIRpt_IMPRESION_DE_COD_DE_BARRAS: fEnumReporteDeArticulosDeInventarioToString = "Impresión de Códigos de Barras"
      Case eAIRpt_CONSULTAR_CORTES: fEnumReporteDeArticulosDeInventarioToString = "Consultar Cortes"
      Case eAIRpt_LISTA_DE_PRECIOS_EN_BSF: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Valor en Bolívar Fuerte"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_EN_BSF: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Existencia y Valor en Bolívar Fuerte"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONONIBILIDAD: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Existencia y Disponibilidad"
      Case eAIRpt_DISPONIBILIDAD_DE_INVENTARIO: fEnumReporteDeArticulosDeInventarioToString = "Disponibilidad"
      Case eAIRpt_DISPONIBILIDAD_DE_INVENTARIO_EN_TRANSITO: fEnumReporteDeArticulosDeInventarioToString = "Disponibilidad En Transito"
      Case eAIRpt_EXISTENCIA_DETALLADA_POR_ARTICULO: fEnumReporteDeArticulosDeInventarioToString = "Existencia Detallada por Artículo"
      Case eAIRpt_EXISTENCIA_MERCANCIA_EN_TRANSITO: fEnumReporteDeArticulosDeInventarioToString = "Existencia Por Mercancía en Tránsito"
      Case eAIRpt_LISTADO_ARTICULOSX_ALMACENX_TIPO: fEnumReporteDeArticulosDeInventarioToString = "Listado de Artículos por Almacen y Tipo de Artículo"
      Case eAIRpt_VALORACION_DEL_INVENTARIO_POR_ALMACEN: fEnumReporteDeArticulosDeInventarioToString = "la valoración del inventario por almacen"
      Case eAIRpt_COSTO_PROMEDIO_GENERAL: fEnumReporteDeArticulosDeInventarioToString = "Costo Promedio General "
      Case eAIRpt_COSTOS_ARTICULOS_COMPUESTOS: fEnumReporteDeArticulosDeInventarioToString = "Informe de Costos Producto Compuesto"
      Case eAIRpt_INVENTARIO_POR_CATEGORIA: fEnumReporteDeArticulosDeInventarioToString = "Inventario Por Categoria"
      Case eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_CATEGORIA: fEnumReporteDeArticulosDeInventarioToString = "Inventario Por Categoria"
      Case eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_LINEA_DE_PRODUCTO: fEnumReporteDeArticulosDeInventarioToString = "Inventario Por Categoria (Linea De Producto)"
      Case eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO: fEnumReporteDeArticulosDeInventarioToString = "Disponibilidad de Artículo de Inventario"
      Case eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO_POR_ALMACEN: fEnumReporteDeArticulosDeInventarioToString = "Disponibilidad de Artículo de Inventario Por Almacen"
      Case eAIRpt_GANANCIA_POR_PRODUCTO_SEGUN_LEY_COSTO: fEnumReporteDeArticulosDeInventarioToString = "Ganancia por Producto Segun Ley Costo"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_MARCA: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Marca"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_MARCA: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Existencia y Marca"
      Case eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONIBILIDAD_Y_MARCA: fEnumReporteDeArticulosDeInventarioToString = "Lista de Precios con Existencia, Disponibilidad y Marca"
      Case Else: fEnumReporteDeArticulosDeInventarioToString = ""
   End Select
End Function

Public Function fConfigurarDatosDelReporteListadoDeArticulos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTADO_DE_ARTICULOS)
   NombreDelReporte = "Listado de Artículos de Inventario"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteListadoDeArticulos = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteListadoDeArticulos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteExistenciaPorTallaYColor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTADO_ARTICULO_TALLA_COLOR)
   NombreDelReporte = "Listado de Artículos de Inventario"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblArticulo", "Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "precioSinIva", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio2", "", "precioSinIva2", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio3", "", "precioSinIva3", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio4", "", "precioSinIva4", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodArtTallaYColor", "", "CodArtTallaYColor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionTalla", "", "DescripcionTalla"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionColor", "", "DescripcionColor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHArticulo", "CodArt", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteExistenciaPorTallaYColor = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteExistenciaPorTallaYColor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteArticulosDeInventario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valDetallado As Boolean, ByVal valConMonedaExtranjera, ByVal valPreciosSinIva As Boolean, ByVal valCodigoInicial As String, ByVal valCodigoFinal As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   If valDetallado Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_ARTICULO_DE_INVENTARIO_DETALLADO)
      NombreDelReporte = "Artículos de Inventario - Detallado"
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_ARTICULO_DE_INVENTARIO_RESUMIDO)
      NombreDelReporte = "Artículos de Inventario - Resumido"
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblDesdeHastaCodigoDeArticulo", "Del Artículo " & valCodigoInicial & " al " & valCodigoFinal
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTipoDearticulo", "Tipo de Artículo"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblDesdeHastaCodigoDeArticulo", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valPreciosSinIva Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosConOSinIva", "Precios Sin " & gGlobalization.fPromptIVA
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosConOSinIva", "Precios Con " & gGlobalization.fPromptIVA
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBaseImponible", "", "PorcentajeBaseImponible"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible1", "", "CampoDefinible1"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible2", "", "CampoDefinible2"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible3", "", "CampoDefinible3"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible4", "", "CampoDefinible4"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible5", "", "CampoDefinible5"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeArticuloEnum", "", "TipoDeArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeArticulo", "", "TipoDeArticuloStr"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "Precio", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio2", "", "Precio2", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio3", "", "Precio3", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio4", "", "Precio4", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionProdCompuestos", "", "DescripcionProdCompuesto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitarioME", "", "MeCostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMePrecio", "", "MePrecio", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMePrecio2", "", "MePrecio2", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMePrecio3", "", "MePrecio3", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMePrecio4", "", "MePrecio4", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUnidadDeVenta", "", "UnidadDeVenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadMinima", "", "CantidadMinima", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadMaxima", "", "CantidadMaxima", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkUsaME", True
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteArticulosDeInventario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteArticulosDeInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteMovimientosDeInventario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCodigoArticulo As String, ByVal valNombreCompaniaParaInformes As String, ByVal valExistenciaInicial As Double, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_MOVIMIENTOS_INVENTARIO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_MOVIMIENTOS_INVENTARIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoProducto", "Código del Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtExistenciaInicial", gUtilReports.fNumToStringConSeparadorDeMiles(valExistenciaInicial, False, gUtilReports.fConvertStringToCurrency(gProyParametrosCompania.GetCantidadDeDecimalesStr))
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeMovimiento", "", "TipoOperacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumento", "", "NoDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEntradas", "", "CantidadEntrante", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSalidas", "", "CantidadSaliente", False, ddTXRight, formatoNDecimales
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteMovimientosDeInventario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteMovimientosDeInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteVencimientoDeProductos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_VENCIMIENTO_DE_PRODUCTOS)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_VENCIMIENTO_DE_PRODUCTOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código del Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDeVencimiento", "", "FechaDeVencimiento", eFT_DATE
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteVencimientoDeProductos = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteVencimientoDeProductos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteGananciaPorProducto(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_GANANCIA_POR_PRODUCTO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_GANANCIA_POR_PRODUCTO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código del Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioDeVenta", "", "PrecioSinIva", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGanancia", "", "Ganancia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentaje", "", "Porcentaje"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteGananciaPorProducto = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteGananciaPorProducto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fConfigurarDatosDelReporteGananciaPorProductoSegunLeyCosto(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_GANANCIA_POR_PRODUCTO_SEGUN_LEY_COSTO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_GANANCIA_POR_PRODUCTO_SEGUN_LEY_COSTO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código del Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         'gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioDeVenta", "", "PrecioSinIva", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGanancia", "", "Ganancia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentaje", "", "MargenGananciaXLey"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcGastosAdmisibles", "", "porcGastosAdmisibles", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtGastosAdmisibles", "", "GastosAdmisibles", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoSegunLey", "", "CostoSegunLey", False, ddTXRight, formatoNDecimales
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteGananciaPorProductoSegunLeyCosto = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteGananciaPorProductoSegunLeyCosto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteListaDePrecios(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNivelDePrecio As String, ByVal valEnMonedaExtranjera As Boolean, _
                     ByVal valPreciosSinIva As Boolean, ByVal valStatus As Integer, ByVal valAgruparPorLineaDeProducto As Boolean, ByVal valOrdernadoPor As String, ByVal valImprimirTodasLasLineas As Boolean, ByVal valChkExistencia As Boolean, _
                     ByVal valNombreCompaniaParaInformes As String, ByVal valFN_Precio1 As String, ByVal valFN_Precio2 As String, ByVal valFN_Precio3 As String, ByVal valFN_Precio4 As String, ByVal valConDisponibiliad As Boolean, _
                     ByVal valConMarca As Boolean, ByRef gGlobalization As Object, ByVal valUsaCamposDefinibles As Boolean, ByVal gProyParametrosCompania As Object, ByVal valMonedaReporte As String, _
                     ByVal valTasaMoneda As String, ByVal valUsaMultiMoneda As Boolean, ByVal valEsMonedaLocalATasaDelDia As Boolean, ByVal valNombreMonedaLocal As String) As Boolean

   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   Const ENBsF As String = "EnBsf"
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   If valChkExistencia Then
      If Now < #1/1/2008# Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_EN_BSF)
         NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_EN_BSF)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA)
         NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA)
      End If
      If valConDisponibiliad Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONONIBILIDAD)
         NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONONIBILIDAD)
         If valConMarca Then
            nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONIBILIDAD_Y_MARCA)
            NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_DISPONIBILIDAD_Y_MARCA)
         End If
      Else
         If valConMarca Then
            nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_MARCA)
            NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_CON_EXISTENCIA_Y_MARCA)
         End If
      End If
   Else
      If Now < #1/1/2008# Then
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_EN_BSF)
         NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_EN_BSF)
      Else
         nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS)
         NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS)
         If valConMarca Then
            nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTA_DE_PRECIOS_CON_MARCA)
            NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTA_DE_PRECIOS_CON_MARCA)
         End If
      End If
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", valFN_Precio1, False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valStatus = enum_StatusArticulo.eSA_VIGENTE Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Listado de Precios de Artículos Vigentes"
         ElseIf valStatus = enum_StatusArticulo.eSA_DESINCORPORADO Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Listado de Precios de Artículos Desincorporados"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         If valEnMonedaExtranjera And valEsMonedaLocalATasaDelDia Then
            If valNombreMonedaLocal <> "" Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaDeLaLista", "Precios en " & valNombreMonedaLocal & " a la Tasa del Día"
            Else
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaDeLaLista", "Precios en Moneda Local a la Tasa del Día"
            End If
         ElseIf valEnMonedaExtranjera And Not valEsMonedaLocalATasaDelDia Then
            If valMonedaReporte <> "" Then
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaDeLaLista", "Precios en " & valMonedaReporte
            Else
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaDeLaLista", "Precios en Moneda Extranjera"
            End If
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaDeLaLista", "Precios en " & valNombreMonedaLocal & ""
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMonedaReporte", "Moneda Original:"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblValMonedaReporte", valMonedaReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTasaDeCambio", "Tasa de Cambio:"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblValTasaDeCambio", valTasaMoneda
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkUsaMultimoneda", valUsaMultiMoneda, ""
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkUsaTasaDelDia", valEsMonedaLocalATasaDelDia, ""
         If valPreciosSinIva Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosConOSinIva", "Precios Sin " & gGlobalization.fPromptIVA
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosConOSinIva", "Precios Con " & gGlobalization.fPromptIVA
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", valFN_Precio1, False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio" & ENBsF, "", valFN_Precio1 & ENBsF, False, ddTXRight, formatoNDecimales
         If valFN_Precio2 <> "" Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio2", "", valFN_Precio2, False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio3", "", valFN_Precio3, False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio4", "", valFN_Precio4, False, ddTXRight, formatoNDecimales
            
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio2" & ENBsF, "", valFN_Precio2 & ENBsF, False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio3" & ENBsF, "", valFN_Precio3 & ENBsF, False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio4" & ENBsF, "", valFN_Precio4 & ENBsF, False, ddTXRight, formatoNDecimales
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPrecio", "Nivel de " & valNivelDePrecio
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio2", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio3", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio4", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio2", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio3", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio4", False
            
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio2" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio3" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio4" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio2" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio3" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio4" & ENBsF, False

         End If
         If Mid(valFN_Precio1, 1, 2) = "Me" Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio2" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio3" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecio4" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio2" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio3" & ENBsF, False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblPrecio4" & ENBsF, False
         End If
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkApruparPorLineaDeProducto", valAgruparPorLineaDeProducto, ""
         If valAgruparPorLineaDeProducto Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "LineaDeProducto", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", True
         End If
         If valImprimirTodasLasLineas Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLinea", "Todas", ""
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLinea", "", "LineaDeProducto"
         End If
         If valStatus = enum_StatusArticulo.eSA_VIGENTE Or valStatus = enum_StatusArticulo.eSA_DESINCORPORADO Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txStatus", "", "Status"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txStatus", "Todos", ""
         End If
         If valAgruparPorLineaDeProducto Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAgrupar", "Si", ""
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAgrupar", "No", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtOrdenado", valOrdernadoPor, ""
         If valConDisponibiliad Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtReserva", "", "CantArtReservado", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDisponible", "", "Disponibiliad", False, ddTXRight, formatoNDecimales
         End If
         If valChkExistencia = True Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
            If valFN_Precio2 <> "" Then
               gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
            Else
               gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
            End If
            If valConMarca Then
              gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMarca", "", "Marca"
            End If
         Else
            If Now < #1/1/2008# Then
               gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
            Else
               gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
            End If
            If valConMarca Then
              gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMarca", "", "Marca"
            End If
         End If
         If valChkExistencia = False And valFN_Precio2 = "" Then
            gUtilReports.sChangeNumericPropertyForControl refRpt, "lblPrecio", 9135, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "txtPrecio", 9135, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "lblDescripcion", 6885, eNPC_WIDTH
            gUtilReports.sChangeNumericPropertyForControl refRpt, "txtDescripcion", 6885, eNPC_WIDTH
         End If
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
      If valUsaCamposDefinibles Then
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCampoDefinible1", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCampoDefinible2", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCampoDefinible3", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCampoDefinible4", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCampoDefinible5", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCampoDefinible1", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCampoDefinible2", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCampoDefinible3", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCampoDefinible4", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCampoDefinible5", True
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinible1", gProyParametrosCompania.GetNombreCampoDefinibleInventario1
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinible2", gProyParametrosCompania.GetNombreCampoDefinibleInventario2
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinible3", gProyParametrosCompania.GetNombreCampoDefinibleInventario3
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinible4", gProyParametrosCompania.GetNombreCampoDefinibleInventario4
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampoDefinible5", gProyParametrosCompania.GetNombreCampoDefinibleInventario5
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible1", "", "CampoDefinible1"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible2", "", "CampoDefinible2"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible3", "", "CampoDefinible3"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible4", "", "CampoDefinible4"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible5", "", "CampoDefinible5"
         If valNivelDePrecio <> "Todos(as)" And valChkExistencia And valConDisponibiliad And valConMarca Then
            gUtilReports.sChangeNumericPropertyForControl refRpt, "lblCampoDefinible1", 9090, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "txtCampoDefinible1", 9090, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "lblCampoDefinible2", 10170, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "txtCampoDefinible2", 10170, eNPC_LEFT
         End If
         If valChkExistencia = False And valFN_Precio2 = "" Then
            gUtilReports.sChangeNumericPropertyForControl refRpt, "lblPrecio", 7250, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "txtPrecio", 7250, eNPC_LEFT
            gUtilReports.sChangeNumericPropertyForControl refRpt, "lblDescripcion", 5000, eNPC_WIDTH
            gUtilReports.sChangeNumericPropertyForControl refRpt, "txtDescripcion", 5000, eNPC_WIDTH
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteListaDePrecios = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteListaDePrecios", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteHojaDeTrabajoDePedido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaAlmacen As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_HOJA_DE_TRABAJO_DEL_PEDIDO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_HOJA_DE_TRABAJO_DEL_PEDIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código del Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valUsaAlmacen Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAlmacen", "", "NombreAlmacen"
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblAlmacen", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtAlmacen", False
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUnidadDeVenta", "", "UnidadDeVenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaInicial", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblEnBlanco1", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblEnBlanco2", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblEnBlanco3", ""
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteHojaDeTrabajoDePedido = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteHojaDeTrabajoDePedido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteProductosPorDebajoDeExistencia(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_PRODUCTOS_POR_DEBAJO_EXIST_MINIMA)
   NombreDelReporte = "Artículos por Debajo de la Existencia Mínima"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadMinima", "", "CantidadMinima", False, ddTXRight, formatoNDecimales
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteProductosPorDebajoDeExistencia = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteProductosPorDebajoDeExistencia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteArticulosSinVenta(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim NombreDelReporte As String
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_ARTICULOS_SIN_VENTA)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_ARTICULOS_SIN_VENTA)
   NombreDelReporte = "Artículos Sin Venta"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaUltimaVenta", "", "Fecha", eFT_DATE
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteArticulosSinVenta = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteArticulosSinVenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteExistenciaPorArticulo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaAlmacen As Boolean, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_EXISTENCIA_POR_ARTICULO)
   NombreDelReporte = "Existencia por Artículo"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Cantidad", False, ddTXRight, formatoNDecimales
         If Not valUsaAlmacen Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoAlmacen", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNombreAlmacen", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtNombreAlmacen", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoAlmacen", False
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteExistenciaPorArticulo = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteExistenciaPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteExistenciaPorAlmacen(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_EXISTENCIA_POR_ALMACEN)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_EXISTENCIA_POR_ALMACEN)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteExistenciaPorAlmacen = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteExistenciaPorAlmacen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCostoPorAlmacen(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valEnMonedaExtranjera As Boolean, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_COSTO_POR_ALMACEN)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_COSTO_POR_ALMACEN)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Cantidad", False, ddTXRight, formatoNDecimales
         If valEnMonedaExtranjera Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMoneda", "Costo en Moneda Extranjera"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "MeCostoUnitario", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoTotal", "", "CostoTotalME", False, ddTXRight, formatoNDecimales
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblMoneda", "Costo en Moneda Local"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoTotal", "", "CostoTotal", False, ddTXRight, formatoNDecimales
         End If
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalExistencia", "Cantidad", eSF_SUM, "GroupHeader1", eSR_ALL, eST_SUB_TOTAL, gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteCostoPorAlmacen = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCostoPorAlmacen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteNotasDeESEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valDetallado As Boolean, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_NOTA_ES_ENTRE_FECHAS)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_NOTA_ES_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeOperacion", "", "TipodeOperacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkImprimirDetalle", valDetallado
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteNotasDeESEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteNotasDeESEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteTransferenciasEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                     ByVal valSqlDelReporte As String, _
                                                                      ByVal valDetallado As Boolean, _
                                                                       ByVal valFechaInicial As Date, _
                                                                        ByVal valFechaFinal As Date, _
                                                                         ByVal valNombreCompaniaParaInformes As String, _
                                                                          ByVal valNombreCampoRollo As String, _
                                                                           ByVal valNombreCampoSerial As String, _
                                                                            ByVal valUsaSerial As Boolean, _
                                                                             ByVal valUsaRollo As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_TRANSFERENCIAS_ENTRE_FECHAS)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_TRANSFERENCIAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacenEntrada", "", "CodigoAlmacenEntrada"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacenEntrada", "", "NombreAlmacenEntrada"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacenSalida", "", "CodigoAlmacenSalida"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacenSalida", "", "NombreAlmacenSalida"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForCheckBox refRpt, "chkImprimirDetalle", valDetallado
         If valDetallado Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "Line2", True
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "Line2", False
         End If
         If valUsaSerial Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", valNombreCampoSerial
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", False
         End If
         If valUsaRollo Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", valNombreCampoRollo
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", False
         End If

         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteTransferenciasEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteTransferenciasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFormatoDeConteoFisico(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_FORMATO_DE_CONTEO_FISICO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_FORMATO_DE_CONTEO_FISICO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadActual", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblEnBlanco", ""
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteFormatoDeConteoFisico = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteFormatoDeConteoFisico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteConteoFisico(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_CONTEO_FISICO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_CONTEO_FISICO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoDeArtículo", "Código de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoDeConteo", "", "ConsecutivoConteo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSoloUnaLinea", "", "SoloUnaLinea"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMuestra", "", "TipoArticuloInv"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaEnSistema", "", "ExistenciaEnElComputador", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaFisica", "", "ExistenciaFisica", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferencia", "", "Diferencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaEnSistemaSerialRollo", "", "ExistenciaEnElComputador", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaFisicaSerialRollo", "", "ExistenciaFisica", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaSerialRollo", "", "Diferencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHConsecutivoConteo", "ConsecutivoConteo", ddGrpAll, ddRepeatOnPageIncludeNoDetail, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalleArticulo", "CodigoArticulo", ddGrpNone, ddRepeatNone, False, ddNPNone
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteConteoFisico = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteConteoFisico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteConteosFisicoEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_CONTEO_FISICO_ENTRE_FECHAS)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_CONTEO_FISICO_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtConsecutivo", "", "ConsecutivoConteo"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreOperador", "", "NombreOperador"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteConteosFisicoEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteConteosFisicoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteTransferencia(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, _
                                                          ByVal valNombreCompaniaParaInformes As String, _
                                                           ByRef gGlobalization As Object, _
                                                            ByVal valNombreCampoRollo As String, _
                                                             ByVal valNombreCampoSerial As String, _
                                                              ByVal valUsaSerial As Boolean, _
                                                               ByVal valUsaRollo As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_TRANSFERENCIA)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_TRANSFERENCIA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoArticulo", "Código de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacenSalida", "", "CodigoAlmacenSalida"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacenSalida", "", "NombreAlmacenSalida"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacenEntrada", "", "CodigoAlmacenEntrada"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacenEntrada", "", "NombreAlmacenEntrada"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComentarios", "", "Comentarios"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIva", False, ddTXLeft, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotal", "", "Total", False, ddTXLeft, formatoNDecimales
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTransferencia", "Total", eSF_SUM, "GroupFooter1", eSR_GROUP, eST_SUB_TOTAL
         If valUsaSerial Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", valNombreCampoSerial
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", False
         End If
         If valUsaRollo Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", valNombreCampoRollo
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", False
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT:
   fConfigurarDatosDelReporteTransferencia = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteTransferencia ", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteRegistroDetalladoDeInventario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_REGSITRO_DETALLADO)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_REGSITRO_DETALLADO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigo", "Código Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaAnterior", "", "ExistenciaAnterior", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEntradas", "", "Entrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSalidas", "", "Salida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetiro", "", "Retiro", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAutoconsumo", "", "Autoconsumo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "ExistenciaActual", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEntradasMonedas", "", "EntradaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSalidasMonedas", "", "SalidaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtRetiroMonedas", "", "RetiroMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAutoconsumoMonedas", "", "AutoconsumoMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaMoneda", "", "ExistenciaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "Costo_Unitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHArticulo", "Codigo", ddGrpAll, ddRepeatNone, False, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteRegistroDetalladoDeInventario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteRegistroDetalladoDeInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteExistenciaEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_EXISTENCIA_ENTRE_FECHAS)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_EXISTENCIA_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCodigoProducto", "Código del Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEntradas", "", "CantidadEntrante", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotalEntradas", "", "CantidadEntrante", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalEntradas", "", "CantidadEntrante", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSalidas", "", "CantidadSaliente", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotalSalidas", "", "CantidadSaliente", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSalidas", "", "CantidadSaliente", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotalExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaInicial", "", "ExistenciaAnterior", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubTotalExistenciaInicial", "", "ExistenciaAnterior", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalExistenciaInicial", "", "ExistenciaAnterior", False, ddTXRight, formatoNDecimales
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteExistenciaEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteExistenciaEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteValoracionDeInventario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valConMonedaExtranjera, ByVal valPreciosSinIva As Boolean, ByVal valCodigoInicial As String, ByVal valCodigoFinal As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_VALORACION_DE_INVENTARIO_ME)
      NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_VALORACION_DE_INVENTARIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblDesdeHastaCodigoDeArticulo", "Del Artículo " & valCodigoInicial & " al " & valCodigoFinal
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblDesdeHastaCodigoDeArticulo", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valPreciosSinIva Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosConOSinIva", "Precios Sin " & gGlobalization.fPromptIVA
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPreciosConOSinIva", "Precios Con " & gGlobalization.fPromptIVA
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeArticuloEnum", "", "TipoDeArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeArticulo", "", "TipoDeArticuloStr"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "Precio", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtValoracion", "", "Valoracion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtValorDeVenta", "", "ValorDeVenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtValorizadoConIva", "", "ValorizadoConIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIvaValorizado", "", "IvaValorizado"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitarioME", "", "MeCostoUnitario", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioME", "", "MePrecio", False, ddTXRight, formatoNDecimales
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtValoracionME", "", "MeValoracion"
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtValorDeVentaME", "", "MeValorDeVenta"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtValorizadoConIvaTotalLinea", "ValorizadoConIva", eSF_SUM, "GHLinea", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtIvaValorizadoTotalLinea", "IvaValorizado", eSF_SUM, "GHLinea", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtValorizadoConIvaTotal", "ValorizadoConIva", eSF_SUM, "GHLinea", eSR_GROUP, eST_GRAND_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtIvaValorizadoTotal", "IvaValorizado", eSF_SUM, "GHLinea", eSR_GROUP, eST_GRAND_TOTAL
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteValoracionDeInventario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteValoracionDeInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteArticulosConReservaPorFacturacion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTADO_ARTICULOS_CON_RESERVA_POR_FACTURACION)
   NombreDelReporte = "Listado de Artículos con Reserva en Facturas"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "llblArticulo", "Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArt", "", "CodArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantReservada", "", "CantReservada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHArticulo", "CodArt", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         exito = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDelReporteArticulosConReservaPorFacturacion = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteArticulosConReservaPorFacturacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteListadoArticulosPorRetirarEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTADO_ARTICULOS_POR_RETIRAR_ENTRE_FECHAS)
   NombreDelReporte = "Listado de Artículos por Retirar entre Fechas"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "llblArticulo", "Artículo"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArt", "", "CodArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "fechaRetiro", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantReservada", "", "CantReservada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHNumero", "NumeroDocumento", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         exito = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDelReporteListadoArticulosPorRetirarEntreFechas = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteListadoArticulosPorRetirarEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteRegistroImpresionDeCodDeBarras(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNombreDelFileXML As String, ByVal valNombreDelReporte As String, ByVal valFechaHoy As Date, ByVal valPrecioSinIva As Boolean, ByVal valSimboloML As String, ByVal valSimboloME As String) As Boolean
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If gUtilReports.fLoadLayout(refRpt, valNombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForBarcode refRpt, "bcCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         If (valPrecioSinIva) Then
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioSinIvaML" 'se deja para compatibilidad con la versión vieja de la plantilla
         Else
            gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "PrecioConIvaML" 'se deja para compatibilidad con la versión vieja de la plantilla
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioSinIvaML", "", "PrecioSinIvaML"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioConIvaML", "", "PrecioConIVAML"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIvaML", "", "IvaML"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioSinIvaME", "", "PrecioSinIvaME"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioConIvaME", "", "PrecioConIvaME"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIvaME", "", "IvaME"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAlicuotaIva", "", "AlicuotaIvaValor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtUnidadDeVenta", "", "UnidadDeVenta"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFecha", valFechaHoy
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible1", "", "CampoDefinible1"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible2", "", "CampoDefinible2"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible3", "", "CampoDefinible3"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible4", "", "CampoDefinible4"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCampoDefinible5", "", "CampoDefinible5"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lneSimboloML", valSimboloML
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lneSimboloME", valSimboloME
         
         gUtilMargins.sAsignarMargenesParticularesYLosGrabaSiEsNecesario refRpt, valNombreDelReporte, True, 0.5, True, 0.5, True, 0.5, True, 0.25, True, ddOPortrait, False, True, 8.5, True, 11
         exito = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDelReporteRegistroImpresionDeCodDeBarras = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteRegistroImpresionDeCodDeBarras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteConsultaDeCortes(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_CONSULTAR_CORTES)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_CONSULTAR_CORTES)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
        
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoCompuesto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Disponibilidad"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtUltimaCompra", "", "CantidadUltimaCompra"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, NombreDelReporte
         exito = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDelReporteConsultaDeCortes = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteConsultaDeCortes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteDetalleDelInventarioPermanenteEnUnidadesFisicas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                                           ByVal valSqlDelReporte As String, _
                                                                                            ByVal valNombreCompaniaParaInformes As String, _
                                                                                             ByVal valFechaInicial As Date, _
                                                                                              ByVal valFechaFinal As Date) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_EN_UNIDADES_FISICAS)
   NombreDelReporte = "Detalle del Inventario Permanente en Unidades Físicas"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeDocumento", "", "NumeroDeDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeOperacion", "", "TipoDeOperacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUnidadDeMedida", "", "UnidadDeVenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicial", "", "ExistenciaInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEntradas", "", "Entradas", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSalidas", "", "Salidas", False, ddTXRight, formatoNDecimales

         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteDetalleDelInventarioPermanenteEnUnidadesFisicas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteDetalleDelInventarioPermanenteEnUnidadesFisicas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteDetalleDelInventarioPermanenteValorizado(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_DETALLE_DEL_INVENTARIO_PERMANENTE_VALORIZADO)
  NombreDelReporte = "Detalle del Inventario Permanente Valorizado"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtcodigo", "", "codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtdescripcion", "", "descripcion"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUnidadDeVenta", "", "UnidadDeVenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtfecha", "", "fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txttipodedocumento", "", "tipodedocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaInicial", "", "ExistenciaInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoInicial", "", "CostoInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoInicial", "", "MontoInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtentrada", "", "entrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoEntrada", "", "MontoEntrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtsalidad", "", "salidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSalidad", "", "MontoSalidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtexistenciaFinal", "", "existenciaFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoFinal", "", "CostoFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFinal", "", "MontoFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAcumuladoCantidades", "", "existenciaFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAcumuladoMontos", "", "MontoFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtConstoUnitarioEntrada", "", "ConstoUnitarioEntrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitarioSalida", "", "CostoUnitarioSalida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecArticulo", "codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalEntradas", "entrada", eSF_SUM, "GFSecArticulo", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoEntradas", "MontoEntrada", eSF_SUM, "GFSecArticulo", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalSalidas", "salidad", eSF_SUM, "GFSecArticulo", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontosSalidas", "MontoSalidad", eSF_SUM, "GFSecArticulo", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalEntradas", "", "entrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoEntradas", "", "MontoEntrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalSalidas", "", "salidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontosSalidas", "", "MontoSalidad", False, ddTXRight, formatoNDecimales
         
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GFSecArticulo", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
 fConfigurarDatosDelReporteDetalleDelInventarioPermanenteValorizado = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteDetalleDelInventarioPermanenteValorizado = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteDetalleDelInventarioPermanenteValorizado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDisponibilidad(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valVerMercanciaEnTransito As Boolean, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valVerMercanciaEnTransito Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_DISPONIBILIDAD_DE_INVENTARIO_EN_TRANSITO)
      NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_DISPONIBILIDAD_DE_INVENTARIO_EN_TRANSITO)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_DISPONIBILIDAD_DE_INVENTARIO)
      NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_DISPONIBILIDAD_DE_INVENTARIO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         'gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodArt"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         
'         If valAgruparPorLineaDeProducto Then
'            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "LineaDeProducto", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
'            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", True
'            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", True
'         End If
''         If valAgruparPorLineaDeProducto Then
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAgrupar", "Si", ""
'         Else
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAgrupar", "No", ""
'         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtReserva", "", "CantArtReservado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDisponible", "", "Disponibiliad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTransito", "", "Transito"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDisponibilidad = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDisponibilidad", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteExistenciaDetalladaPorArticulo(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                          ByVal valSqlDelReporte As String, _
                                                                           ByVal valNombreCompaniaParaInformes As String, _
                                                                            ByVal gGlobalization As Object, _
                                                                             ByVal valSinonimoTalla As String, _
                                                                              ByVal valSinonimoColor As String, _
                                                                               ByVal valSinonimoSerial As String, _
                                                                                ByVal valSinonimoRollo As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_EXISTENCIA_DETALLADA_POR_ARTICULO)
   NombreDelReporte = "Existencia Detallada por Artículo"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblStatusArticulo", "Status de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTalla", valSinonimoTalla, False
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblColor", valSinonimoColor, False
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", valSinonimoSerial, False
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", valSinonimoRollo, False
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCategoria", "", "Categoria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMarca", "", "Marca"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusArticulo", "", "StatusdelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTalla", "", "DescripcionTalla"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtColor", "", "DescripcionColor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMuestra", "", "TipoArticuloInv"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "ExistenciaMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservada", "", "CantReservadaMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDisponibilidad", "", "DisponibilidadMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaDetalle", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservadaDetalle", "", "CantReservada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDisponibilidadDetalle", "", "DisponibilidadDetalle", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHLineaDeProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMarca", "Marca", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHStatusArticulo", "StatusdelArticulo", ddGrpAll, ddRepeatOnPageIncludeNoDetail, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalleArticulo", "Codigo", ddGrpNone, ddRepeatNone, False, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalleArticuloTCSR", "", ddGrpNone, ddRepeatAll, False, ddNPNone
                 
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteExistenciaDetalladaPorArticulo = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteExistenciaDetalladaPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteExistenciaMercanciaEnTransito(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                          ByVal valSqlDelReporte As String, _
                                                                           ByVal valNombreCompaniaParaInformes As String, _
                                                                            ByVal gGlobalization As Object) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_EXISTENCIA_MERCANCIA_EN_TRANSITO)
   NombreDelReporte = "Existencia Mercancía en Tránsito"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblStatusArticulo", "Status de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCategoria", "", "Categoria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusdelArticulo", "", "StatusdelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservada", "", "CantidadReservada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadEnTransito", "", "CantidadEnTransito", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDisponible", "", "CantidadDisponible", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecLineaProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecStatusdelArticulo", "StatusdelArticulo", ddGrpAll, ddRepeatOnPageIncludeNoDetail, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteExistenciaMercanciaEnTransito = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteExistenciaMercanciaEnTransito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfigurarDatosDelReporteCostoUltimaCompra(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_COSTO_ULTIMA_COMPRA)
   NombreDelReporte = fEnumReporteDeArticulosDeInventarioToString(eAIRpt_REGSITRO_DETALLADO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtcodigo", "", "codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtdescripcion", "", "descripcion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtfecha", "", "fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txttipodedocumento", "", "tipodedocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaInicial", "", "ExistenciaInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoInicial", "", "CostoInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoInicial", "", "MontoInicial", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtentrada", "", "entrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoEntrada", "", "MontoEntrada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtsalidad", "", "salidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSalidad", "", "MontoSalidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtexistenciaFinal", "", "existenciaFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoFinal", "", "CostoFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFinal", "", "MontoFinal", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecArticulo", "codigo", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCostoUltimaCompra = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteCostoUltimaCompra = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCostoUltimaCompra", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteListadoArticulosxAlmacenxTipo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_LISTADO_ARTICULOSX_ALMACENX_TIPO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCompania", "", "NombreCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoArticuloInv", "", "TipoArticuloInv"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescTipoArticuloInv", "", "DescTipoArticuloInv"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioConIva", "", "PrecioConIva", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioSinIva", "", "PrecioSinIva", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalExisAlmacen_SecAlmacen", "", "TotalExisAlmacen", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalExisTipo_SecTipoArticulo", "", "TotalExisTipo", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_TotalExisReporte_SecCompania", "", "TotalExisReporte", False, ddTXRight, formatoNDecimales
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecAlmacen", "CodigoAlmacen", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTipoArticulo", "TipoArticuloInv", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalExisAlmacen_SecAlmacen", "Existencia", eSF_SUM, "GHSecAlmacen", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalExisTipo_SecTipoArticulo", "Existencia", eSF_SUM, "GHSecTipoArticulo", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_TotalExistencia", "Existencia", eSF_SUM, "GHSecCompania", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fEnumReporteDeArticulosDeInventarioToString(eAIRpt_LISTADO_ARTICULOSX_ALMACENX_TIPO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteListadoArticulosxAlmacenxTipo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteListadoArticulosxAlmacenxTipo = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteListadoArticulosxAlmacenxTipo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfigurarDatosDelReporteCostoPromedioPorAlmacen(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_VALORACION_DEL_INVENTARIO_POR_ALMACEN)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
        gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", "", "NombreAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoTotal", "", "CostoTotal"
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Existencia_SecAlmacen", "Existencia", eSF_SUM, "GHSecAlmacen", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoUnitario_SecAlmacen", "CostoUnitario", eSF_SUM, "GHSecAlmacen", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecAlmacen", "CostoTotal", eSF_SUM, "GHSecAlmacen", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Existencia_SecTotalGeneral", "Existencia", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoUnitario_SecTotalGeneral", "CostoUnitario", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "CostoTotal", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Existencia_SecTotalGeneral", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoUnitario_SecTotalGeneral", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "", "CostoTotal"

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Existencia_SecAlmacen", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoUnitario_SecAlmacen", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecAlmacen", "", "CostoTotal"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecAlmacen", "CodigoAlmacen", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fEnumReporteDeArticulosDeInventarioToString(eAIRpt_VALORACION_DEL_INVENTARIO_POR_ALMACEN)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCostoPromedioPorAlmacen = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteCostoPromedioPorAlmacen = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCostoPromedioPorAlmacen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCostoPromedioGeneral(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
    nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_COSTO_PROMEDIO_GENERAL)
  If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoTotal", "", "CostoTotal"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Existencia_SecTotalGeneral", "Existencia", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoUnitario_SecTotalGeneral", "CostoUnitario", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "CostoTotal", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Existencia_SecTotalGeneral", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoUnitario_SecTotalGeneral", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoTotal_SecTotalGeneral", "", "CostoTotal"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fEnumReporteDeArticulosDeInventarioToString(eAIRpt_COSTO_PROMEDIO_GENERAL)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCostoPromedioGeneral = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteCostoPromedioGeneral = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCostoPromedioGeneral", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfigurarDatosDelReporteCostosArticulosCompuestos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valTipoDePrecio As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_COSTOS_ARTICULOS_COMPUESTOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPVP", valTipoDePrecio
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtcompuesto", "", "Compuesto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionCompuesto", "", "DescripcionCompuesto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPVP", "", "PVP"
         
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAporte", "", "Aporte"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoIndividual", "", "CostoIndividual"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAporteDelIten", "", "AporteDelIten"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_AporteDelIten_Seccompuesto", "AporteDelIten", eSF_SUM, "GHSeccompuesto", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_AporteDelIten_Seccompuesto", "", "AporteDelIten"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "compuesto", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSeccompuesto", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fEnumReporteDeArticulosDeInventarioToString(eAIRpt_COSTOS_ARTICULOS_COMPUESTOS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gConvert", gConvert)
        
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteCostosArticulosCompuestos = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteCostosArticulosCompuestos = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCostosArticulosCompuestos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteInventarioPorCategoria(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String, ByVal valUsaFiltro As Boolean, ByVal valFiltrarPor As String, ByVal valParaFiltrar As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML  As String
   On Error GoTo h_ERROR
   exito = False
  If (valUsaFiltro) Then
  If (valFiltrarPor = "Categoria") Then
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_CATEGORIA)
  Else
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_INVENTARIO_POR_CATEGORIA_FILTRADO_POR_LINEA_DE_PRODUCTO)
  End If
  Else
  nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_INVENTARIO_POR_CATEGORIA)
  End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCategoria", "", "Categoria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "Linea De Producto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtReserva", "", "Reserva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDisponible", "", "Disponible"
        If (valUsaFiltro) Then
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCategoria", valFiltrarPor, ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fEnumReporteDeArticulosDeInventarioToString(eAIRpt_INVENTARIO_POR_CATEGORIA)
        Else
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fEnumReporteDeArticulosDeInventarioToString(eAIRpt_INVENTARIO_POR_CATEGORIA)
        End If
         exito = True
      End If
   End If
    fConfiguraLasSeccionesDelReporteInventarioPorCategoria = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraLasSeccionesDelReporteInventarioPorCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteDisponibilidadArticuloInventario(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                          ByVal valSqlDelReporte As String, _
                                                                           ByVal valNombreCompaniaParaInformes As String, _
                                                                            ByVal gGlobalization As Object, _
                                                                             ByVal valAgrupaPor As Integer, _
                                                                              ByVal valFiltroLineaProducto As Boolean, _
                                                                               ByVal valFiltroCategoria As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   Const CM_AGRUPA_POR_ESTATUS = 0
   Const CM_AGRUPA_POR_LINEA_DE_PRODUCTO = 1
   Const CM_AGRUPA_POR_CATEGORIA = 2
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO)
   NombreDelReporte = "Disponibilidad de Artículo de Inventario"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblStatusArticulo", "Status de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCategoria", "", "Categoria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusdelArticulo", "", "StatusdelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservada", "", "CantidadReservada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDisponible", "", "CantidadDisponible", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMuestra", "", "TipoArticuloInv"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTalla", "", "DescripcionTalla"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtColor", "", "DescripcionColor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoOriginal", "", "CodigoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionOriginal", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoGrupo", "", "CodigoDelArticulo"
        
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaTotal", "", "ExistenciaMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservadaTotal", "", "CantReservadaMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDisponibleTotal", "", "DisponibilidadMaster", False, ddTXRight, formatoNDecimales
        
         If valFiltroLineaProducto Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecLineaProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHSecLineaProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", True
         End If
         If valFiltroCategoria Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecLineaProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCategoria", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCategoria", True
         End If
         If valAgrupaPor = CM_AGRUPA_POR_LINEA_DE_PRODUCTO Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecLineaProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", True
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCategoria", False
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtExistenciaTotalLinea", "Existencia", eSF_SUM, "GHSecLineaProducto", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadReservadaTotalLinea", "CantidadReservada", eSF_SUM, "GHSecLineaProducto", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadDisponibleTotalLinea", "CantidadDisponible", eSF_SUM, "GHSecLineaProducto", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
         ElseIf valAgrupaPor = CM_AGRUPA_POR_CATEGORIA Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCategoria", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCategoria", True
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecLineaProducto", False
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtExistenciaTotalCategoria", "Existencia", eSF_SUM, "GHSecCategoria", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadReservadaTotalCategoria", "CantidadReservada", eSF_SUM, "GHSecCategoria", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadDisponibleTotalCategoria", "CantidadDisponible", eSF_SUM, "GHSecCategoria", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
         ElseIf valAgrupaPor = CM_AGRUPA_POR_ESTATUS Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecStatusdelArticulo", "StatusdelArticulo", ddGrpAll, ddRepeatOnPageIncludeNoDetail, True, ddNPNone
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHSecLineaProducto", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHSecCategoria", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecLineaProducto", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCategoria", False
         End If
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHGrupoSerial", "CodigoOriginal", ddGrpNone, ddRepeatNone, False, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHGrupoSerialDetalle", "", ddGrpNone, ddRepeatAll, False, ddNPNone
         
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteDisponibilidadArticuloInventario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteDisponibilidadArticuloInventario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteDisponibilidadArticuloInventarioPorAlmacen(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                          ByVal valSqlDelReporte As String, _
                                                                           ByVal valNombreCompaniaParaInformes As String, _
                                                                            ByVal gGlobalization As Object, _
                                                                             ByVal valAgrupaPor As Integer, _
                                                                              ByVal valFiltroLineaProducto As Boolean, _
                                                                               ByVal valFiltroCategoria As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   Dim formatoNDecimales As String
   Const CM_AGRUPA_POR_ESTATUS = 0
   Const CM_AGRUPA_POR_LINEA_DE_PRODUCTO = 1
   Const CM_AGRUPA_POR_CATEGORIA = 2
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeArticuloDeInventario(eAIRpt_DISPONIBILIDAD_ARTICULO_INVENTARIO_POR_ALMACEN)
   NombreDelReporte = "Disponibilidad de Artículo de Inventario"
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", NombreDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblStatusArticulo", "Status de Artículo"
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCategoria", "", "Categoria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusdelArticulo", "", "StatusdelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistencia", "", "Existencia", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservada", "", "CantidadReservada", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDisponible", "", "CantidadDisponible", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMuestra", "", "TipoArticuloInv"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTalla", "", "DescripcionTalla"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtColor", "", "DescripcionColor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoOriginal", "", "CodigoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionOriginal", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoGrupo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionAlmacen", "", "DescripcionAlmacen"
        
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtExistenciaTotal", "", "ExistenciaMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadReservadaTotal", "", "CantReservadaMaster", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDisponibleTotal", "", "DisponibilidadMaster", False, ddTXRight, formatoNDecimales
        
         If valFiltroLineaProducto Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecLineaProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHSecLineaProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", True
         End If
         If valFiltroCategoria Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecLineaProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCategoria", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCategoria", True
         End If
         If valAgrupaPor = CM_AGRUPA_POR_LINEA_DE_PRODUCTO Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecLineaProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", True
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCategoria", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCategoria", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCategoria", False
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtExistenciaTotalLinea", "Existencia", eSF_SUM, "GHSecLineaProducto", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadReservadaTotalLinea", "CantidadReservada", eSF_SUM, "GHSecLineaProducto", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadDisponibleTotalLinea", "CantidadDisponible", eSF_SUM, "GHSecLineaProducto", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
         ElseIf valAgrupaPor = CM_AGRUPA_POR_CATEGORIA Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCategoria", "Categoria", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCategoria", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCategoria", True
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecLineaProducto", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblLineaDeProducto", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtLineaDeProducto", False
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtExistenciaTotalCategoria", "Existencia", eSF_SUM, "GHSecCategoria", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadReservadaTotalCategoria", "CantidadReservada", eSF_SUM, "GHSecCategoria", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtCantidadDisponibleTotalCategoria", "CantidadDisponible", eSF_SUM, "GHSecCategoria", eSR_GROUP, eST_SUB_TOTAL, formatoNDecimales
         ElseIf valAgrupaPor = CM_AGRUPA_POR_ESTATUS Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecStatusdelArticulo", "StatusdelArticulo", ddGrpAll, ddRepeatOnPageIncludeNoDetail, True, ddNPNone
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHSecLineaProducto", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GHSecCategoria", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecLineaProducto", False
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCategoria", False
         End If
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHGrupoSerial", "CodigoOriginal", ddGrpNone, ddRepeatNone, False, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHGrupoSerialDetalle", "", ddGrpNone, ddRepeatAll, False, ddNPNone
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHAlmacen", "CodigoAlmacen", ddGrpNone, ddRepeatNone, False, ddNPNone
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, , NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteDisponibilidadArticuloInventarioPorAlmacen = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteDisponibilidadArticuloInventarioPorAlmacen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



