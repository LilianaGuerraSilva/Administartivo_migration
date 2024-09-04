VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsDefFacturaCotizacion"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsDefFacturaCotizacion"
Private Const CM_MESSAGE_NAME As String = "Def Factura y Cotizaci�n"
Private Const GetGender = Enum_Gender.eg_Male

Enum enum_ClaseDeTrabajoFacturaCotizacion
   eCTFC_Factura = 0
   eCTFC_Cotizacion
   eCTFC_NotaDeEntrega
  End Enum

Public Function getFN_CONSECUTIVO_COMPANIA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CONSECUTIVO_COMPANIA = "ConsecutivoCompania"
End Function

Public Function getFN_NUMERO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO = "Numero"
End Function

Public Function getFN_FECHA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA = "Fecha"
End Function

Public Function getFN_CODIGO_ALMACEN(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CODIGO_ALMACEN = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CODIGO_ALMACEN = "CodigoAlmacen"
   End If
End Function

Public Function getFN_NOMBRE_ALMACEN(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NOMBRE_ALMACEN = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NOMBRE_ALMACEN = "NombreAlmacen"
   End If
End Function

Public Function getFN_NIVEL_DE_PRECIO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NIVEL_DE_PRECIO = "NivelDePrecio"
End Function

Public Function getFN_CODIGO_CLIENTE(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CODIGO_CLIENTE = "CodigoCliente"
End Function

Public Function getFN_NOMBRE_CLIENTE(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NOMBRE_CLIENTE = "NombreCliente"
End Function

Public Function getFN_CODIGO_VENDEDOR(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CODIGO_VENDEDOR = "CodigoVendedor"
End Function

Public Function getFN_CONSECUTIVO_VENDEDOR(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CONSECUTIVO_VENDEDOR = "ConsecutivoVendedor"
End Function

Public Function getFN_NOMBRE_VENDEDOR(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NOMBRE_VENDEDOR = "NombreVendedor"
End Function

Public Function getFN_STATUS_FACTURA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_STATUS_FACTURA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_STATUS_FACTURA = "StatusFactura"
   End If
End Function

Public Function getFN_TIPO_DE_DOCUMENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TIPO_DE_DOCUMENTO = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_TIPO_DE_DOCUMENTO = "TipoDeDocumento"
   End If
End Function

Public Function getFN_OBSERVACIONES(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_OBSERVACIONES = "Observaciones"
End Function

Public Function getFN_PORCENTAJE_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion)
   getFN_PORCENTAJE_IVA = "PorcentajeIVA"
End Function

Public Function getFN_TOTAL_MONTO_EXENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TOTAL_MONTO_EXENTO = "TotalMontoExento"
End Function

Public Function getFN_TOTAL_BASE_IMPONIBLE(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TOTAL_BASE_IMPONIBLE = "TotalBaseImponible"
End Function

Public Function getFN_TOTAL_RENGLONES(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TOTAL_RENGLONES = "TotalRenglones"
End Function

Public Function getFN_TOTAL_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TOTAL_IVA = "TotalIVA"
End Function

Public Function getFN_TOTAL_FACTURA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_TOTAL_FACTURA = "TotalFactura"
   Else
      getFN_TOTAL_FACTURA = "TotalCotizacion"
   End If
End Function

Public Function getFN_PORCENTAJE_DESCUENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_PORCENTAJE_DESCUENTO = "PorcentajeDescuento"
End Function

Public Function getFN_INSERTADA_MANUALMENTE(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_INSERTADA_MANUALMENTE = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_INSERTADA_MANUALMENTE = "InsertadaManualmente"
   End If
End Function

Public Function getFN_FACTURA_HISTORICA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FACTURA_HISTORICA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_FACTURA_HISTORICA = "FacturaHistorica"
   End If
End Function

Public Function getFN_CANCELADA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CANCELADA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CANCELADA = "Cancelada"
   End If
End Function

Public Function getFN_CODIGO_NOTA1(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CODIGO_NOTA1 = "CodigoNota1"
End Function

Public Function getFN_DESCRIPCION_NOTA1(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_DESCRIPCION_NOTA1 = "DescripcionNota1"
End Function

Public Function getFN_CODIGO_NOTA2(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CODIGO_NOTA2 = "CodigoNota2"
End Function

Public Function getFN_DESCRIPCION_NOTA2(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_DESCRIPCION_NOTA2 = "DescripcionNota2"
End Function

Public Function getFN_USAR_DIRECCION_FISCAL(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_USAR_DIRECCION_FISCAL = "UsarDireccionFiscal"
End Function

Public Function getFN_NO_DIR_DESPACHO_AIMPRIMIR(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NO_DIR_DESPACHO_AIMPRIMIR = "NoDirDespachoAimprimir"
End Function

Public Function getFN_USA_MAQUINA_FISCAL(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_USA_MAQUINA_FISCAL = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_USA_MAQUINA_FISCAL = "UsaMaquinaFiscal"
   End If
End Function

Public Function getFN_CODIGO_MAQUINA_REGISTRADORA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CODIGO_MAQUINA_REGISTRADORA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CODIGO_MAQUINA_REGISTRADORA = "CodigoMaquinaRegistradora"
   End If
End Function

Public Function getFN_NUMERO_DESDE(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_DESDE = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_DESDE = "NumeroDesde"
   End If
End Function

Public Function getFN_NUMERO_HASTA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_HASTA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_HASTA = "NumeroHasta"
   End If
End Function

Public Function getFN_NUMERO_CONTROL_HASTA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_CONTROL_HASTA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_CONTROL_HASTA = "NumeroControlHasta"
   End If
End Function

Public Function getFN_MONTO_IVA_RETENIDO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MONTO_IVA_RETENIDO = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_MONTO_IVA_RETENIDO = "MontoIvaRetenido"
   End If
End Function

Public Function getFN_FECHA_APLICACION_RET_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA_APLICACION_RET_IVA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_FECHA_APLICACION_RET_IVA = "FechaAplicacionRetIVA"
   End If
End Function

Public Function getFN_NUMERO_COMPROBANTE_RET_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_COMPROBANTE_RET_IVA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_COMPROBANTE_RET_IVA = "NumeroComprobanteRetIVA"
   End If
End Function

Public Function getFN_FECHA_COMPROBANTE_RET_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA_COMPROBANTE_RET_IVA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_FECHA_COMPROBANTE_RET_IVA = "FechaComprobanteRetIVA"
   End If
End Function

Public Function getFN_SE_RETUVO_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_SE_RETUVO_IVA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_SE_RETUVO_IVA = "SeRetuvoIVA"
   End If
End Function

Public Function getFN_NOMBRE_OPERADOR(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NOMBRE_OPERADOR = "NombreOperador"
End Function

Public Function getFN_FECHA_ULTIMA_MODIFICACION(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA_ULTIMA_MODIFICACION = "FechaUltimaModificacion"
End Function

Public Function getFN_MONEDA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MONEDA = "Moneda"
End Function

Public Function getFN_CAMBIO_ABOLIVARES(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CAMBIO_ABOLIVARES = ""
   If valClaseDeTrabajo = eCTFC_Factura Or valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_CAMBIO_ABOLIVARES = "CambioABolivares"
   End If
End Function

Public Function getFN_MONTO_DEL_ABONO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MONTO_DEL_ABONO = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_MONTO_DEL_ABONO = "MontoDelAbono"
   End If
End Function

Public Function getFN_FECHA_DE_VENCIMIENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA_DE_VENCIMIENTO = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_FECHA_DE_VENCIMIENTO = "FechaDeVencimiento"
   End If
End Function

Public Function getFN_CONDICIONES_DE_PAGO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CONDICIONES_DE_PAGO = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CONDICIONES_DE_PAGO = "CondicionesDePago"
   End If
End Function

'desde aqui solo cotizacion
Public Function getFN_CONFIRMADA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CONFIRMADA = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_CONFIRMADA = "Confirmada"
   End If
End Function

Public Function getFN_CONTACTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CONTACTO = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_CONTACTO = "Contacto"
   End If
End Function

Public Function getFN_MODIFICADO_POR_OPERADOR(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MODIFICADO_POR_OPERADOR = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_MODIFICADO_POR_OPERADOR = "ModificadoPorOperador"
   End If
End Function

Public Function getFN_MONTO_DESCUENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MONTO_DESCUENTO = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_MONTO_DESCUENTO = "MontoDescuento"
   End If
End Function

Public Function getFN_PROMOCION(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_PROMOCION = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_PROMOCION = "promocion"
   End If
End Function

'CAMPOS DE RENGLON DEL DOCUMENTO
Public Function getFN_CONSECUTIVO_RENGLON(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CONSECUTIVO_RENGLON = "ConsecutivoRenglon"
End Function

Public Function getFN_RENGLON_CODIGO_ARTICULO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_RENGLON_CODIGO_ARTICULO = "Articulo"
   Else
      getFN_RENGLON_CODIGO_ARTICULO = "CodigoArticulo"
   End If
End Function

Public Function getFN_RENGLON_NUMERO_DOCUMENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_RENGLON_NUMERO_DOCUMENTO = "NumeroFactura"
   Else
      getFN_RENGLON_NUMERO_DOCUMENTO = "NumeroCotizacion"
   End If
End Function

Public Function getFN_RENGLON_CONSECUTIVO_COMPANIA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_CONSECUTIVO_COMPANIA = "ConsecutivoCompania"
End Function

Public Function getFN_RENGLON_DESCRIPCION(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_DESCRIPCION = "Descripcion"
End Function

Public Function getFN_RENGLON_ALICUOTA_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_ALICUOTA_IVA = "AlicuotaIVA"
End Function

Public Function getFN_RENGLON_CANTIDAD(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_CANTIDAD = "Cantidad"
End Function

Public Function getFN_RENGLON_PRECIO_SIN_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_PRECIO_SIN_IVA = "PrecioSinIVA"
End Function

Public Function getFN_RENGLON_PRECIO_CON_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_PRECIO_CON_IVA = "PrecioConIVA"
End Function

Public Function getFNL_RENGLON_MONTO_BRUTO_SIN_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFNL_RENGLON_MONTO_BRUTO_SIN_IVA = "MontoBrutoSinIva"
End Function

Public Function getFNL_RENGLON_MONTO_BRUTO_CON_IVA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFNL_RENGLON_MONTO_BRUTO_CON_IVA = "MontoBrutoConIva"
End Function

Public Function getFN_RENGLON_PORCENTAJE_DESCUENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_PORCENTAJE_DESCUENTO = "PorcentajeDescuento"
End Function

Public Function getFNL_RENGLON_MONTO_DESCUENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFNL_RENGLON_MONTO_DESCUENTO = "MontoDescuento"
End Function

Public Function getFN_RENGLON_TOTAL_RENGLON(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_TOTAL_RENGLON = "TotalRenglon"
End Function

Public Function getFN_RENGLON_PORCENTAJE_BASE_IMPONIBLE(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_RENGLON_PORCENTAJE_BASE_IMPONIBLE = "PorcentajeBaseImponible"
End Function

'CAMPOS DEFINIBLES
Public Function getFN_CAMPOS_DEFINIBLES_NUMERO_FACTURA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CAMPOS_DEFINIBLES_NUMERO_FACTURA = "NumeroFactura"
   Else
      getFN_CAMPOS_DEFINIBLES_NUMERO_FACTURA = "NumeroCotizacion"
   End If
End Function

'CAMPOS RENGLON OTROS CARGOS
Public Function getFN_OTROS_CARGOS_NUMERO_FACTURA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_OTROS_CARGOS_NUMERO_FACTURA = "NumeroFactura"
   Else
      getFN_OTROS_CARGOS_NUMERO_FACTURA = "NumeroCotizacion"
   End If
End Function

Public Function getFN_OTROS_CARGOS_COMO_APLICA_AL_TOTAL_FACTURA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_OTROS_CARGOS_COMO_APLICA_AL_TOTAL_FACTURA = "ComoAplicaAlTotalFactura"
   Else
      getFN_OTROS_CARGOS_COMO_APLICA_AL_TOTAL_FACTURA = "ComoAplicaAlTotalCotizacion"
   End If
End Function

Public Function GetTableName(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      GetTableName = "factura"
   Else
      GetTableName = "cotizacion"
   End If
End Function

Public Function GetRenglonTableName(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      GetRenglonTableName = "renglonFactura"
   Else
      GetRenglonTableName = "renglonCotizacion"
   End If
End Function

Public Function GetCamposDefiniblesTableName(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      GetCamposDefiniblesTableName = "camposDefFactura"
   Else
      GetCamposDefiniblesTableName = "camposDefCotizacion"
   End If
End Function

Public Function GetRenglonOtrosCargosTableName(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   If valClaseDeTrabajo = eCTFC_Factura Then
      GetRenglonOtrosCargosTableName = "renglonDetalleDeOtrosCargosFactura"
   Else
      GetRenglonOtrosCargosTableName = "renglonOtrosCargosDeCotizacion"
   End If
End Function

Public Function getFN_SEGUIMIENTO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_SEGUIMIENTO = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_SEGUIMIENTO = "Seguimiento"
   End If
End Function

Public Function getFN_FECHA_DE_PROXIMA_LLAMADA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA_DE_PROXIMA_LLAMADA = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_FECHA_DE_PROXIMA_LLAMADA = "FechaDeProximaLlamada"
   End If
End Function

Public Function getFN_ESTADO_DE_LA_COTIZACION(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_ESTADO_DE_LA_COTIZACION = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_ESTADO_DE_LA_COTIZACION = "EstadoDeLaCotizacion"
   End If
End Function

Public Function getFN_FORMA_DE_ENVIO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FORMA_DE_ENVIO = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_FORMA_DE_ENVIO = "FormaDeEnvio"
   End If
End Function

Public Function getFN_FECHA_DE_ULTIMA_EMISION_DE_FAX(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FECHA_DE_ULTIMA_EMISION_DE_FAX = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_FECHA_DE_ULTIMA_EMISION_DE_FAX = "FechaDeUltimaEmisionDeFax"
   End If
End Function

Public Function getFN_NUMERO_DE_VECES_FAXEADO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_DE_VECES_FAXEADO = ""
   If valClaseDeTrabajo = eCTFC_Cotizacion Then
      getFN_NUMERO_DE_VECES_FAXEADO = "NumeroDeVecesFaxeado"
   End If
End Function

Public Function getFN_CAMPO_EXTRA_EN_RENGLON_FACTURA1(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CAMPO_EXTRA_EN_RENGLON_FACTURA1 = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CAMPO_EXTRA_EN_RENGLON_FACTURA1 = "CampoExtraEnRenglonFactura1"
   End If
End Function

Public Function getFN_CAMPO_EXTRA_EN_RENGLON_FACTURA2(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_CAMPO_EXTRA_EN_RENGLON_FACTURA2 = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_CAMPO_EXTRA_EN_RENGLON_FACTURA2 = "CampoExtraEnRenglonFactura2"
   End If
End Function

Public Function getFN_FORMA_DE_LA_INICIAL(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FORMA_DE_LA_INICIAL = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_FORMA_DE_LA_INICIAL = "FormaDeLaInicial"
   End If
End Function

Public Function getFN_PORCENTAJE_DE_LA_INICIAL(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_PORCENTAJE_DE_LA_INICIAL = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_PORCENTAJE_DE_LA_INICIAL = "PorcentajeDeLaInicial"
   End If
End Function

Public Function getFN_NUMERO_DE_CUOTAS(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_DE_CUOTAS = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_DE_CUOTAS = "NumeroDeCuotas"
   End If
End Function

Public Function getFN_MONTO_DE_LAS_CUOTAS(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MONTO_DE_LAS_CUOTAS = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_MONTO_DE_LAS_CUOTAS = "MontoDeLasCuotas"
   End If
End Function

Public Function getFN_MONTO_ULTIMA_CUOTA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_MONTO_ULTIMA_CUOTA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_MONTO_ULTIMA_CUOTA = "MontoUltimaCuota"
   End If
End Function

Public Function getFN_FORMA_DE_PAGO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_FORMA_DE_PAGO = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_FORMA_DE_PAGO = "FormaDePago"
   End If
End Function

Public Function getFN_NUM_DIAS_DE_VENCIMIENTO1A_CUOTA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUM_DIAS_DE_VENCIMIENTO1A_CUOTA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUM_DIAS_DE_VENCIMIENTO1A_CUOTA = "NumDiasDeVencimiento1aCuota"
   End If
End Function

Public Function getFN_EDITAR_MONTO_CUOTA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_EDITAR_MONTO_CUOTA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_EDITAR_MONTO_CUOTA = "EditarMontoCuota"
   End If
End Function

Public Function getFN_NUMERO_CONTROL(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_CONTROL = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_CONTROL = "NumeroControl"
   End If
End Function

Public Function getFN_TIPO_DE_TRANSACCION(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TIPO_DE_TRANSACCION = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_TIPO_DE_TRANSACCION = "TipoDeTransaccion"
   End If
End Function

Public Function getFN_NUMERO_FACTURA_AFECTADA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_FACTURA_AFECTADA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_FACTURA_AFECTADA = "NumeroFacturaAfectada"
   End If
End Function

Public Function getFN_NUMERO_PLANILLA_EXPORTACION(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_NUMERO_PLANILLA_EXPORTACION = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_NUMERO_PLANILLA_EXPORTACION = "NumeroPlanillaExportacion"
   End If
End Function

Public Function getFN_TIPO_DE_VENTA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_TIPO_DE_VENTA = ""
   If valClaseDeTrabajo = eCTFC_Factura Then
      getFN_TIPO_DE_VENTA = "TipoDeVenta"
   End If
End Function

Public Function getFN_GENERADA_POR_NOTA_ENTREGA(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_GENERADA_POR_NOTA_ENTREGA = "GeneradaPorNotaEntrega"
End Function

Public Function getFN_EMITIDA_EN_FACTURA_NUMERO(ByVal valClaseDeTrabajo As enum_ClaseDeTrabajoFacturaCotizacion) As String
   getFN_EMITIDA_EN_FACTURA_NUMERO = "EmitidaEnFacturaNumero"
End Function
