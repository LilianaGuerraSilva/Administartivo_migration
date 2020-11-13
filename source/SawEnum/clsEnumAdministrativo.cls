VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsEnumAdministrativo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Enum menusDelSistemaAdministrativo
' Administrativo
   me_PRINCIPALSAW = 1
   me_CXC
   me_COMPANIA
   me_TABLASSAW
   me_INVENTARIO
   me_CXP
   me_BANCO
   me_PARAMETROS
   me_PRIVATE
   me_VEHICULOS
   me_IMPORTAREXPORTAR
   me_CAJA_CHICA_RENDICION
' Contabilidad
   me_PRINCIPALWINCONT
   me_ACTIVOFIJO
   me_TABLASWINCONT
   me_IMPORTEXPORTWINCONT
' me_PARAMETROSWINCONT
   me_CONTABILIZACION
   me_COMPRAS
   me_INFORME_DE_AUDITORIA
   me_CONEXIONISLR
' Menu Caja
   me_PRINCIPALCAJA
'Contabilidad
   me_CONEXIONAXI
   me_ESTRUCTURA_COSTOS
'Separacion Por Migracion AOS
   me_CONVERSION_RESPALDO_LOTE
   me_TABLASSAW_CONT
'Produccion
   me_PRODUCCION
End Enum

Public Enum enum_OpcionesParaElReporteListadoDePrecios
   eORLP_PRECIODEVENTAENMONEDALOCAL = 0
   eORLP_PRECIODEVENTAMONEDAEXTRANJERA
   eORLP_PRECIODEVENTAALATASADELDIAENMONEDALOCAL
End Enum

Public Enum enum_SystemModules
   eSM_MenuG = 0
   eSM_AlicuotaIVA
   eSM_Almacen
   eSM_Anticipo
   eSM_AnticipoCobrado
   eSM_AnticipoPagado
   eSM_ArticuloInventario
   eSM_Autorizacion
   eSM_Banco
   eSM_Caja
   eSM_CajaApertura
   eSM_CambiarDireccionDeDespacho
   eSM_CamposDefCotizacion
   eSM_CamposDefFactura
   eSM_CambioaMonedaLocal
   eSM_CamposMonedaExtranjera
   eSM_Categoria
   eSM_CategoriaCliente
   eSM_CategoriaProveedor
   eSM_ChequeDeMovBancario
   eSM_CIIU
   eSM_Ciudad
   eSM_ClaveSuperUtilitario
   eSM_Cliente
   eSM_Cobranza
   eSM_Color
   eSM_ComisionCobranzaXVcto
   eSM_Compania
   eSM_Compra
   eSM_ConceptoBancario
   eSM_Conciliacion
   eSM_ConteoFisico
   eSM_Contrato
   eSM_CorreccionDeFechas
   eSM_Cotizacion
   eSM_CuentaBancaria
   eSM_Curso
   eSM_CxC
   eSM_CxP
   eSM_Despacho
   eSM_DiaDeDeclaracionDelIVA
   eSM_DireccionDeDespacho
   eSM_DocumentoCobrado
   eSM_DocumentoPagado
   eSM_ExistenciaPorAlmacen
   eSM_ExistenciaPorGrupo
   eSM_Factura
   eSM_FacturasVendedor
   eSM_FaxMasivo
   eSM_FormaDelCobro
   eSM_GeneracionDeContratos
   eSM_GrupoTallaColor
   eSM_LineaDeProducto
   eSM_Llamada
   eSM_LoteAdm
   eSM_MailFax
   eSM_MaquinaFiscal
   eSM_MarcaVehiculo
   eSM_ModeloVehiculo
   eSM_Moneda
   eSM_MonedaLocal
   eSM_MovimientoBancario
   eSM_NotaDeEntradaSalida
   eSM_NotaFinal
   eSM_OPFalsoRetencion
   eSM_OrdenDeCompra
   eSM_OrdenDeServicio
   eSM_OtrosCargosDeFactura
   eSM_Pago
   eSM_Pais
   eSM_Parametros
   eSM_ParametrosCompania
   eSM_Participante
   eSM_PlanillaForma00030
   eSM_PreguntasMasComunes
   eSM_ProductoCompuesto
   eSM_PropAnalisisVenc
   eSM_Proveedor
   eSM_ReglasDeContabilizacion
   eSM_RenglonArtCompuestoCot
   eSM_RenglonArtCompuestoFac
   eSM_RenglonCobroDeFactura
   eSM_RenglonComisionesDeVendedor
   eSM_RenglonCompra
   eSM_RenglonCompraXSerial
   eSM_RenglonConteoFisico
   eSM_RenglonContrato
   eSM_RenglonCotizacion
   eSM_RenglonExistenciaAlmacen
   eSM_RenglonFactura
   eSM_RenglonGrupoColor
   eSM_RenglonGrupoTalla
   eSM_RenglonNotaES
   eSM_RenglonOtrosCargosDeCotizacion
   eSM_RenglonOtrosCargosDeFactura
   eSM_RenglonTransferencia
   eSM_RespaldarRestaurar
   eSM_ResumenDiarioVentas
   eSM_RetDocumentoPagado
   eSM_RetirosACuenta
   eSM_RetPago
   eSM_RevisionDeData
   eSM_SectorDeNegocio
   eSM_TablaRetencion
   eSM_Talla
   eSM_Talonario
   eSM_TipoDeCurso
   eSM_TipoProveedor
   eSM_Transferencia
   eSM_UnidadDeVenta
   eSM_UrbanizacionZP
   eSM_Usuario
   eSM_Vendedor
   eSM_VersionPrograma
   eSM_Visita
   eSM_ZonaCobranza
   eSM_GestionDeCobranza
   eSM_ComprobanteRetencionIVA
   eSM_DebitoBancario
   eSM_ServiciosAlRealizarVisita
   eSM_PlanillaForma00030Nueva
   eSM_PDT
   esM_AnticipoAdelanto
   eSM_Rendicion
   eSM_DetalleDeRendicion
   eSM_CajaChica
   eSM_Adelanto
   eSM_Detracciones
   eSM_TipoDeDocumentoLey
   eSM_Vehiculo
   eSM_ControlDespacho
   eSM_DetalleDeControlDespacho
   'modulos de winret
   eSM_PlanillaDec
   eSM_PlantillaRET
   eSM_ValorUT
   eSM_TarifaN2
   eSM_ARCV
   eSM_RelacionAnual
   eSM_RenglonARCV
   eSM_ConversionContabilidad
   eSM_Contabilizar
   eSM_Balances
   eSM_Informes
   eSM_ImportarExportar
   eSM_CerrarPeriodo
   eSM_CierreCostoPeriodo
   esM_DetalleDeConciliacion
   esM_InteligenciaDeNegocio
   esM_InformesVendedores
   eSM_Beneficiario
   eSM_SolicitudesDePago
   eSM_RenglonSolicitudesDePago
   eSM_Integracion
   eSM_Municipio
   eSM_MunicipioCiudad
   eSM_ClasificadorActividadEconomica
   eSM_ImpuestoMunicipalRetNavigator
   eSM_RenglonImpuestoMunicipalRetNav
   eSM_NotaDeEntrega
   eSM_GuiaDeRemision
   eSM_Ubigeo
   eSM_Provincia
   eSM_Departamento
   eSM_MotivoTraslado
   eSM_Conductor
   eSM_DetalleGuiaDeRemision
   eSM_CompraDetalleGasto
   eSM_CargaInicial
   eSM_ExC
   eSM_DetalleExC
   eSM_OrdenDeCompraDetalleArticuloInventario
   eSM_CondicionesDePago
   eSM_ListaDeMateriales
   eSM_ListaDeMaterialesDetalleArticulo
   eSM_OrdenDeProduccion
   eSM_OrdenDeProduccionDetalleArticulo
   eSM_OrdenDeProduccionDetalleMateriales
   
End Enum

Public Enum enum_DiaDeLaSemana
   eDD_DOMINGO = 0
   eDD_LUNES
   eDD_MARTES
   eDD_MIERCOLES
   eDD_JUEVES
   eDD_VIERNES
   eDD_SABADO
End Enum

Public Enum enum_StatusVendedor
   eSV_ACTIVO = 0
   eSV_INACTIVO
   eSV_RESTRINGIDO
End Enum

Public Enum enum_TipoDeContribuyente
   eTD_CONTRIBUYENTE = 0
   eTD_NOCONTRIBUYENTE
End Enum

Public Enum enum_TipoDePersonaFiscal
   eTD_PERSONAJURIDICA = 0
   eTD_PERSONANATURAL
End Enum

Public Enum enum_StatusCliente
   eSC_ACTIVO = 0
   eSC_INACTIVO
   eSC_RESTRINGIDO
   eSC_SUSPENDIDO
   eSC_TIEMPO_SIN_CONTACTO
   eSC_DATOS_DESACTUALIZADOS
End Enum

Public Enum enum_StatusDocumento
   eSD_PORCANCELAR = 0
   eSD_CANCELADO
   eSD_CHEQUEDEVUELTO
   eSD_ABONADO
   eSD_ANULADO
   eSD_REFINANCIADO
End Enum

Public Enum enum_TipoDeTransaccion
   eTD_FACTURA = 0
   eTD_GIRO
   eTD_CHEQUEDEVUELTO
   eTD_NOTADECREDITO
   eTD_NOTADEDEBITO
   eTD_NOTAENTREGA
   eTD_NOASIGNADO
   eTD_BOLETADEVENTA
   eTD_TICKETMAQUINAREGISTRADORA
   eTD_RECIBOPORHONORARIOS
   eTD_LIQUIDACIONDECOMPRA
   eTD_OTROS
   eTD_NOTADECREDITOCOMPROBANTEFISCAL
   eTD_LETRADECAMBIO
End Enum

Public Enum enum_OrigenFacturacionOManual
   eOF_FACTURA = 0
   eOF_MANUAL
   eOF_S10
End Enum

Public Enum enum_FacturaGeneraNotaEntrega
   eFGNE_NoGenerada = 0
   eFGNE_Generada
End Enum

Public Enum enum_FormaDeEscogerCompania
   eFD_PORNOMBRE = 0
   eFD_PORCODIGO
End Enum

Public Enum enum_VerPromocionNoPromocionTodos
   eVer_TodosPromocion = 0
   eVer_Promocion
   eVer_NoPromocion
End Enum

Public Enum enum_StatusCobranza
   eSC_VIGENTE = 0
   eSC_ANULADA
End Enum

Public Enum enum_TipoDeAlicuota
   eTD_EXENTO = 0
   eTD_ALICUOTAGENERAL
   eTD_ALICUOTA2
   eTD_ALICUOTA3
   eTD_EXENTONC
   eTD_ALICUOTAGENERALNC
   eTD_ALICUOTA2NC
   eTD_ALICUOTA3NC
End Enum

Public Enum enum_StatusArticulo
   eSA_VIGENTE = 0
   eSA_DESINCORPORADO
End Enum

Public Enum enum_StatusDetraccion
   eSD_AUTODETRACCION = 0
   eSD_DETRACCIONPORTERCERO
   eSD_NOAPLICADA
End Enum

Public Enum enum_TipoDeProducto
   eTD_TP_NUEVO = 0
   eTD_TP_ACTUALIZACION
   eTD_TP_OTRO
   eTD_TP_POLIZA
End Enum

Public Enum enum_StatusFactura
   eSF_EMITIDA = 0
   eSF_ANULADA
   eSF_BORRADOR
End Enum

Public Enum enum_TipoDocumentoFactura
   eTF_FACTURA = 0
   eTF_NOTADECREDITO
   eTF_NOTADEDEBITO
   eTF_RESUMENDIARIODEVENTAS
   eTF_NOASIGNADO
   eTF_COMPROBANTEFISCAL
   eTF_BOLETA
   eTF_NOTADECREDITOCOMPROBANTEFISCAL
   eTF_NOTAENTREGA
End Enum

Public Enum enum_FormaDePago
   eFP_CONTADO = 0
   eFP_CREDITO
End Enum

Public Enum enum_ContadoCredito
   eCC_CONTADO = 0
   eCC_Credito
   eCC_CLAVE_TEMPORAL
End Enum

Public Enum enum_MonoMultiUsuario
   eMM_MONOUSUARIO = 0
   eMM_MULTIUSUARIO
   eMM_SQL_SERVER
End Enum

Public Enum enum_TipoDeSAW
   eTD_SAW_BASICO = 0
   eTD_SAW_PROFESIONAL
   eTD_SAW_CONTRIBUYENTES_ESPECIALES
End Enum

Public Enum enum_TipoDeAutorizacion
   eTD_NUEVAINSTALACION = 0
   eTD_REINSTALACION
End Enum

Public Enum enum_TipoDeFactura
   eTD_OFICINA = 0
   eTD_DISTRIBUIDOR
   eTD_PROMOCION
End Enum

Public Enum enum_OrigenLlamada
   eOL_CLIENTE = 0
   eOL_PRENSA
   eOL_MAILING
   eOL_REFERIDO
   eOL_RADIO
   eOL_PAGINASAMARRILLAS
   eOL_EVENTO
   eOL_COMUNICADOFAX
   eOL_OFICINA
   eOL_WWWGALACCOM
End Enum

Public Enum enum_Accion
   eAc_NINGUNA = 0
   eAc_COTIZAR
   eAc_FACTURAR
   eAc_PROBLEMA
   eAc_REPOSICION
   eAc_RELLAMAR
End Enum

Public Enum enum_TipoLLamada
   eTL_VENTA = 0
   eTL_SERVICIO
   eTL_OFICINA
   eTL_PROMOCION
   eTL_CLAVE
   eTL_COBRANZA
   eTL_INTERNET
   eTL_POST_VENTA
   eTL_NUEVOS
   eTL_OFFICE_SUPPORT
   eTL_POR_DEFINIR
   eTL_INSTALACION
   eTL_CONFIGURACION
   eTL_IMPRESION
End Enum

Public Enum enum_MananaOTarde
   eMO_MANANA = 0
   eMO_TARDE
   eMO_TODOELDIA
End Enum

Public Enum enum_StatusVisita
   eSV_VIGENTE = 0
   eSV_CANCELADA
   eSV_REASIGNADA
   eSV_REALIZADA
End Enum

Public Enum enum_Tarjeta
   eTa_VISA = 0
   eTa_MASTER
   eTa_AMERICAN
   eTa_DEBITO
End Enum

Public Enum enum_TipoDeAlmacen
   eTD_PRINCIPAL = 0
   eTD_SECUNDARIO
   eTD_CONSIGNACION
   eTD_MERCANCIA_EN_TIENDA
End Enum

Public Enum enum_TipodeOperacion
   eTO_ENTRADA_DE_INVENTARIO = 0
   eTO_SALIDA_DE_INVENTARIO
   eTO_Autoconsumo
   eTO_Retiro
End Enum

Public Enum enum_TipoDeArticulo
   eTD_MERCANCIA = 0
   eTD_SERVICIO
   eTD_PRODUCTO_COMPUESTO
End Enum

Public Enum enum_NivelDePrecio
   eND_PRECIO_1 = 0
   eND_PRECIO_2
   eND_PRECIO_3
   eND_PRECIO_4
End Enum

Public Enum enum_TipoDeServicio
   eTD_ACTUALIZACION = 0
   eTD_ADIESTRAMIENTO
   eTD_DEMOSTRACION
   eTD_GARANTIA
   eTD_INSTALACION
   eTD_REPOSICION
   eTD_OTRO
End Enum

Public Enum enum_StatusDespacho
   eED_PENDIENTE = 0
   eED_ENVIADO
   eED_ANULADO
End Enum

Public Enum enum_Courier
   eCo_MENSAJEROS_RADIO_MRW = 0
   eCo_SUPER_ENVIOS
   eCo_MENSAJERO_GALAC
   eCo_OFICINA
End Enum

Public Enum enum_TipoDeDespacho
   eTD_NORMAL = 0
   eTD_FLETE_DESTINO
   eTD_COD
End Enum

Public Enum enum_LugarDespacho
   eLD_INTERIOR = 0
   eLD_CARACAS
End Enum

Public Enum enum_MotivoDelDespacho
   eMD_FACTURA = 0
   eMD_Reposicion 'eMD_REENVIO
   eMD_DEMO
   eMD_OTROS
   eMD_NOTA_ENTREGA
   eMD_EVENTO
   eMD_DEPOSITO_ENVIO
End Enum

Public Enum enum_StatusPago
   eSP_VIGENTE = 0
   eSP_ANULADA
End Enum

Public Enum enum_TipoDeCtaBancaria
   eTD_CORRIENTE = 0
   eTD_AHORROS
   eTD_ACTIVOS_LIQUIDOS
End Enum

Public Enum enum_StatusCtaBancaria
   eSC_ACTIVA = 0
   eSC_INACTIVA
End Enum

Public Enum enum_IngresoEgreso
   eIE_INGRESO = 0
   eIE_EGRESO
End Enum

Public Enum enum_GeneradoPor
   eGP_USUARIO = 0
   eGP_COBRANZA
   eGP_ORDEN_DE_PAGO
   eGP_CXP
   eGP_DEBITO_BANCARIO
   eGP_CXC
   eGP_GESTION_COBRANZA
   eGP_COMPRA
   eGP_ANTICIPO
   eGP_RECONVERSION_MONETARIA
   eGP_S10
   eGP_RENDICION
   eGP_SOLICITUD_DE_PAGO
   eGP_REPOSICION_DE_CAJACHICA
   eGP_RETENCION_EN_COBRANZA
End Enum

Public Enum enum_Horario
   eHo_DE_800_AM_A_1200_PM = 0
   eHo_DE_900_AM_A_1130_AM
   eHo_DE_130_PM_A_430_PM
   eHo_DE_130_PM_A_530_PM
   eHo_DE_2_PM_A_530_PM
   eHo_DE_1_PM_A_530_PM
   eHo_DE_800_AM_A_530_PM
End Enum

Public Enum enum_StatusParticipante
   eSP_RESERVADO = 0
   eSP_CONFIRMADO
   eSP_ASISTIO
   eSP_CANCELADO
   eSP_NO_ASISTIO
   eSP_LISTA_ESPERA
   eSP_TRASLADADO
End Enum

Public Enum enum_TipoDeCompra
   eTD_COMPRASEXENTAS = 0
   eTD_COMPRASIMPORTACION
   eTD_COMPRASNACIONALES
   eTD_COMPRASEXONERADAS
   eTD_COMPRASNOSUJETAS
   eTD_COMPRASSINDERECHOACREDITO
End Enum

Public Enum enum_CreditoFiscal
   eCF_DEDUCIBLE = 0
   eCF_PRORRATEABLE
   eCF_NoDeducible
End Enum

Public Enum enum_FormaDeCancelarPago
   eFD_EFECTIVO = 0
   eFD_CHEQUE
   eFD_TARJETA
   eFD_DEPOSITO
End Enum

Public Enum enum_BaseCalculoOtrosCargosDeFactura
   eBC_FORMULA = 0
   eBC_MONTO_FIJO
   eBC_INDICAR_MONTO
End Enum

Public Enum enum_BaseFormulaOtrosCargosDeFactura
   eBF_SUBTOTAL = 0
   eBF_BASE_IMPONIBLE
   eBF_TOTALFACTURA
End Enum

Public Enum enum_ComoAplicaOtrosCargosDeFactura
   eCA_SUMA = 0
   eCA_RESTA
   eCA_NO_APLICA_SOLO_INFORMATIVO
End Enum

Public Enum enum_StatusContrato
   eSCo_VIGENTE = 0
   eSCo_DESACTIVADO
End Enum

Public Enum enum_DuracionDelContrato
   eDD_DURACIONFIJA = 0
   eDD_DURACIONINDEFINIDA
End Enum

Public Enum enum_ValorDelRenglon
   eVD_VALORENARCHIVOARTICULOS = 0
   eVD_INDICARELVALOR
End Enum

Public Enum enum_PeriodicidadContratos
   ePC_MENSUAL = 0
   ePC_BIMESTRAL
   ePC_TRIMESTRAL
   ePC_SEMESTRAL
   ePC_ANUAL
   ePC_CUOTAUNICA
End Enum

Public Enum enum_PeriodoDeAplicacion
   ePD_ELDELCONTRATO = 0
   ePD_ESPECIALINDICAR
End Enum

Public Enum enum_StatusCurso
   eSC_DISPONIBLE = 0
   eSC_LLENO
   eSC_CANCELADO
   eSC_DICTADO
End Enum

Public Enum enum_CantidadAgenerar
   eCA_TODOSLOSCONTRATOS = 0
   eCA_UNCONTRATO
End Enum

Public Enum enum_RedondeaOtrunca
   eRO_REDONDEA = 0
   eRO_TRUNCA
End Enum

Public Enum enum_ProvieneDe
   ePd_CxPCxC = 0
   ePd_Pago
   ePd_Cobranza
End Enum

Public Enum enum_ModeloDeFactura
   eMD_FORMALIBRE = 0
   eMD_OTRO
   eMD_IMPRESION_MODO_TEXTO
End Enum

Public Enum enum_TipoDePrefijo
   eTD_SINPREFIJO = 0
   eTD_ANO
   eTD_INDICAR
End Enum

Public Enum enum_QueImprimirDesdePago
   eQI_TODO = 0
   eQI_PAGO_MAS_CHEQUE
   eQI_RETENCION
   eQI_SOLO_PAGO
   eQI_SOLO_CHEQUE
   eQI_RETENCION_IVA
   eQI_COMPROBANTE_MAS_CHEQUE
   eQI_SOLO_CHEQUE_COMPROBANTE
   eQI_SOLO_COMPROBANTE
End Enum

Public Enum enum_PreciosDeLosProductos
   ePD_PRECIOCONIVA = 0
   ePD_PRECIOSINIVA
End Enum

Public Enum enum_FormaDeSeleccionarCompania
   eFD_ESCOGERCOMPANIAALENTRAR = 0
   eFD_ESCOGERULTIMACOMPANIAUSADA
End Enum

Public Enum enum_StatusPregunta
   eSP_POR_RESPONDER = 0
   eSP_RESPONDIDA
   eSP_FALTA_INFORMACION
End Enum

Public Enum enum_TipoDeOrdenDePagoAImprimir
   eTD_ORDENDEPAGOCONCHEQUE = 0
   eTD_SOLOORDENDEPAGO
   eTD_ORDENDEPAGOSEPARADADECHEQUE
End Enum

Public Enum enum_TipoDeComprobanteChequeAImprimir
   eKC_COMBANTECONCHEQUE = 0
   eKC_SOLOORCOMPROBANTE
   eKC_COMPROBANTESEPARADODECHEQUE
End Enum

Public Enum enum_StatusServicio
   eSS_PENDIENTE = 0
   eSS_EN_REVISION
   eSS_NO_TIENE_SOLUCION
   eSS_ENVIADA_AL_CLIENTE
   eSS_REVISADA
End Enum

Public Enum enum_MontoPorcentaje
   eMP_POR_MONTO = 0
   eMP_POR_PORCENTAJE
End Enum

Public Enum enum_StatusCompra
   eSCp_VIGENTE = 0
   eSCp_ANULADA
End Enum

Public Enum enum_StatusConteoFisico
   eSC_PROCESADO = 0
   eSC_POR_PROCESAR
   eSC_ANULAR
End Enum

Public Enum enum_PermitirSobregiro
   eCI_NO_CHEQUEAR_EXISTENCIA = 0
   eCI_PERMITIR_SOBREGIRO
   eCI_NO_PERMITIR_SOBREGIRO
End Enum

Public Enum enum_TipoDeSolicitudDeIngresoDeTasaDeCambio
   eTD_SIEMPREALEMITIRPRIMERAFACTURA = 0
   eTD_SOLOSIESNECESARIO
End Enum

Public Enum enum_CalculoParaComisionesSobreCobranzaEnBaseA
   eCP_MONTO = 0
   eCP_DIASVENCIDOS
   eCP_PORCENTAJE_POR_ARTICULO
   eCP_PORCENTAJE_POR_LINEA_DE_PRODUCTO
   eCP_MIXTO
End Enum

Public Enum enum_ReporteComisionesPorMontoYDiasVenc
   eRC_Sencillo = 0
   eRC_DocumentoCobradosYCobranza
   eRC_SoloDocumentosCobrados
End Enum

Public Enum enum_MotivoDelLote
   eMD_ENTRADA_MANUAL = 0
   eMD_EXPORTACION
   eMD_IMPORTACION
   eMD_RESPALDO
End Enum

Public Enum enum_TipoDeContabilizacion
   eTD_CADA_DOCUMENTO = 0
   eTD_POR_LOTE
End Enum

Public Enum enum_ContabilizacionIndividual
   eCI_INMEDIATA = 0
   eCI_POSPUESTA
End Enum

Public Enum enum_ContabilizacionPorLote
   eCP_DIARIA = 0
   eCP_MENSUAL
End Enum

Public Enum enum_StatusConciliacion
   eSC_ENPROCESO = 0
   eSC_CERRADA
End Enum

Public Enum enum_TipodeMovimientos
   eTM_Todos = 0
   eTM_Conciliados
   eTM_NoConciliados
End Enum

Public Enum enum_StatusDeCotizacionGalac
   eSD_PORCONFIRMAR = 0
   eSD_VOLVERALLAMAR
   eSD_NOCONTESTA
   eSD_REENVIADA
   eSD_FACTURADA
   eSD_RECHAZADA
   eSD_NODEFINIDO
   eSD_CONFIRMADA
   eSD_FACTURADA_EN_OTRA_COTIZACION
End Enum

Public Enum enum_ModoDeEnvio
   eMD_EMAIL = 0
   eMD_FAX
   eMD_IMPRESO
End Enum

Public Enum enum_InfoGalacModoEnvio
   eIG_POR_DEFINIR = 0
   eIG_FAX
   eIG_EMAIL
   eIG_MAIL_Y_FAX
   eIG_NO_POR_AHORA
   eIG_NO_TIENE_FAXMAIL
   eIG_YA_SUSCRITO
End Enum

Public Enum enum_Talonario
   eTa_TALONARIO1 = 0
   eTa_TALONARIO2
End Enum

Public Enum enum_EnviadoVia
   eEV_NO_ENVIADO = 0
   eEV_FAX
   eEV_EMAIL
End Enum

Public Enum enum_TipoDocumentoEnviado
   eTD_VENTA = 0
   eTD_SERVICIO_DE_ENVIO
   eTD_CLAVE
   eTD_COBRANZA
End Enum

Public Enum enum_EnviadorODestinatario
   eEM_ENVIADOR = 0
   eEM_DESTINATARIO
End Enum

Public Enum enum_NivelesVentayCobranza
   eNV_NIVEL1 = 0
   eNV_NIVEL2
   eNV_NIVEL3
   eNV_NIVEL4
   eNV_NIVEL5
End Enum

Enum enum_ComoSeEntero
   eCS_POR_DEFINIR = 0
   eCS_EL_UNIVERSAL
   eCS_EL_NACIONAL
   eCS_ULTIMAS_NOTICIAS
   eCS_REFERIDO
   eCS_CAVEGUIAS
   eCS_INTERNET
   eCS_YA_ES_CLIENTE
   eCS_EVENTOS
   eCS_OTRO
End Enum

Enum enum_CategoriaLlamada
   eCL_POR_DEFINIR = 0
   eCL_ENTRANTE
   eCL_SALIENTE
   eCL_ATENCION_EN_OFICINA
   eCL_FAX
End Enum

Public Enum enum_GenerarMovBancarioDesdeCobro
   eGM_Siempre = 0
   eGM_Preguntar
   eGM_Nunca
End Enum

Public Enum enum_StatusRetencionIVACobranza
   eSR_NO_APLICA = 0
   eSR_PENDIENTE_POR_DISTRIBUIR
   eSR_DISTRIBUIDO
End Enum

Public Enum enum_FormaDeLaInicial
   eFD_POR_MONTO = 0
   eFD_POR_PORCENTAJE
End Enum

Public Enum enum_StatusGestion
   eSG_PENDIENTE = 0
   eSG_REALIZADA
   eSG_FINALIZADA
End Enum

Public Enum enum_ProximoPasoGestion
   ePP_INDICAR = 0
   ePP_LLAMAR
   ePP_COBRAR_MENSAJERO
   ePP_COBRAR_Vendedor
   ePP_RECLAMO
End Enum

Public Enum enum_TipoDeTransaccionDeLibrosFiscales
   eTD_A01_REGISTRO = 0
   eTD_A02_COMPLEMENTO
   eTD_A03_ANULACION
   eTD_A04_AJUSTE
End Enum

Public Enum enum_TipoDeProveedorDeLibrosFiscales
   eTD_CON_RIF = 0
   eTD_SIN_RIF
   eTD_NO_RESIDENCIADO
   eTD_NO_DOMICILIADO
End Enum

Public Enum enum_TipoDocumentoCxP  ' No usar, esta deprecado. Usar enum_TipoDeTransaccion
   eTCxP_FACTURA = 0
   eTCxP_GIRO
   eTCxP_CHEQUEDEVUELTO
   eTCxP_NOTADECREDITO
   eTCxP_NOTADEDEBITO
   eTCxP_NOTADEENTREGA
End Enum

Enum enum_TipoDocumentoIVA
   eTD_TD_FACTURA = 0
   eTD_TD_NOTADEENTREGA
   eTD_TD_NOTADECREDITO       '2
   eTD_TD_NOTADEDEBITO        '3
   eTD_TD_RESUMENDIARIODEVENTAS  '4
   eTD_TD_VENTADEPASAJESAEREOS   '5
   eTD_TD_RESUMENCONTRIBUYENTES  '6
   eTD_TD_FACTURAANULADA         '7
   eTD_TD_RESUMENNOCONTRIBUYENTES   '8
   eTD_TD_CERTIFICACIONES           '9
   eTD_TD_NOTADECREDITORESUMEN      '10
   eTD_TD_RETENCIONRESUMENDIARIODEVENTAS      '11
End Enum

Public Enum enum_TipoDeVenta
   eTD_INTERNA = 0
   eTD_EXPORTACION
   eTD_SIN_DERECHO_A_CREDITO_FISCAL
   eTD_A_CONTRIBUYENTE
   eTD_A_NO_CONTRIBUYENTE
End Enum

Public Enum enum_DondeSeEfectuaLaRetencionIVA
   eDS_NO_RETENIDA = 0
   eDS_CXP
   eDS_PAGO
End Enum

Public Enum enum_DondeEfectuoContabilizacionRetIVA
   eDC_NoContabilizada = 0
   eDC_CxP
   eDC_Pago
End Enum

Public Enum enum_FormaDeReiniciarComprobanteRetIVA
   eFD_SIN_ESCOGER = 0
   eFD_POR_MES
   eFD_POR_ANO
   eFD_AL_COMPLETAR
End Enum

Public Enum enum_TipoDeContribuyenteIVA
'   eTD_FORMAL = 0
'   eTD_ORDINARIO
'   eTD_ESPECIAL
'   eTD_ENTES_PUBLICOS
'   eTD_CONVERSION
   
   eTD_FORMAL = 0
   eTD_ORDINARIOS
   eTD_ESPECIAL
   eTD_ENTES_PUBLICOS
   eTD_CONVERSION
End Enum

Public Enum enum_Periodo
   eP_Mensual = 0
   eP_Trimestral
   eP_Semestral
End Enum

Public Enum Enum_TablaCiiu
   eTC_Ninguna = 0
   eTC_TablaAnteriorA2004
   eTC_Tabla2004
End Enum

Public Enum enum_ModeloPlanillaForma00030
   eMPF00030_F03_Grafibond = 0
   eMPF00030_F03_Olivenca
   eMPF00030_F04_Grafibond
   eMPF00030_F05_Grafibond
End Enum

Enum enum_StatusMaquinaFiscal
   eSMF_ACTIVA = 0
   eSMF_INACTIVA
End Enum

Public Enum enum_ResumenDeLibros
   eRDL_SOLO_VENTAS = 0
   eRDL_SOLO_COMPRAS
   eRDL_VENTAS_Y_COMPRAS
End Enum

Public Enum enum_ProductoLlamada
   eNPLL_IVA = 0
   eNPLL_AXI
   eNPLL_INFOCONT
   eNPLL_DPN
   eNPLL_DPJ
   eNPLL_FACTURA  ' 5
   eNPLL_AXP
   eNPLL_NOMINA
   eNPLL_RET
   eNPLL_WCONTABILIDAD  '9 WINDOWS
   eNPLL_ISV
   eNPLL_ADMInterno
   eNPLL_SAW    '12 windows
   eNPLL_WINIVA
   eNPLL_WINRET
   eNPLL_COMBO
   eNPLL_SERVICIO
   eNPLL_SAWINTEGRADO '17 INTEGRADO CON CONTABILIDAD
   eNPLL_IVAGALAC
   eNPLL_DER
   eNPLL_PRESUPUESTO
   eNPLL_NOMINA_WINDOWS
End Enum

Public Enum enum_AgruparLibroDeVentas
   eALDV_NINGUNO = 0
   eALDV_TALONARIO
   eALDV_MAQUINA
   eALDV_TALONARIO_Y_MAQUINA
   eALDV_MAQUINA_Y_TALONARIO '4
End Enum

Public Enum enum_EntidadAExportarOImportar
   eEAEI_PROVEEDOR = 0
   eEAEI_CLIENTE
   eEAEI_ARTICULO_INVENTARIO
   eEAEI_CxC
   eEAEI_CxP
   eEAEI_FACTURA
   eEAEI_VENDEDOR
   eEAEI_MERCANTIL
   eEAEI_BOD
   eEAEI_CxC_ESPECIAL
   eEAEI_COTIZACION
   eEAEI_BANESCO
End Enum

Public Enum enum_FormaDeOrdenarDetalleFactura
   eFD_COMO_FUE_CARGADA = 0
   eFD_POR_CODIGO_DE_ARTICULO
   eFD_POR_DESCRIPCION_DE_ARTICULO
End Enum

Public Enum enum_FormaDeOrdenarCodigosString
   eFD_NORMAL = 0
   eFD_ESPECIAL
End Enum

Public Enum enum_StatusAnticipo
   eSDA_VIGENTE = 0
   eSDA_ANULADO
   eSDA_PARCIALMENTE_USADO
   eSDA_COMPLETAMENTE_USADO
   eSDA_COMPLETAMENTE_DEVUELTO
   eSDA_PARCIALMENTE_DEVUELTO
End Enum

Public Enum enum_TipoDeAnticipo
   eTDA_COBRADO = 0
   eTDA_PAGADO
   eTDA_ADELANTO
End Enum

Public Enum enum_TipoDeFormaDePago
   eTDA_EFECTIVO = 0
   eTDA_TARJETA
   eTDA_CHEQUE
   eTDA_DEPOSITO
   eTDA_ANTICIPO
End Enum

Public Enum enum_PuertoSerial
   ePS_COM1 = 1
   ePS_COM2
   ePS_COM3
   ePS_COM4
   ePS_COM5
   ePS_COM6
   ePS_COM7
   ePS_COM8
   ePS_COM9
   ePS_COM10
   ePS_COM11
   ePS_COM12
   ePS_COM13
   ePS_COM14
   ePS_COM15
   ePS_COM16
End Enum

Public Enum enum_TipoDeNivelDePrecios
   eTDNDP_PorUsuario = 0
   eTDNDP_PorCliente
End Enum

Public Enum enum_ComprobanteConCheque
   eCC_ComprobanteConCheque = 0
   eCC_SoloComprobante
   eCC_ComprobanteSeparadoDeCheque
End Enum

Public Enum enum_ConceptoBancarioPorDefecto
   eCB_DEBITO_BANCARIO_AUTOMATICO = 1
   eCB_REVERSO_AUTOMATICO_COBRANZA
   eCB_REV_AUTOMATICO_ANT_COBRADO
   eCB_REV_AUTOMATICO_ANT_PAGADO
   eCB_ANTICIPO_COBRADO
   eCB_ANTICIPO_PAGADO
   eCB_COBRO_DIRECTO_DE_FACT
   eCB_REVERSO_AUTOMATICO_PAGO
   eCB_TRANSERENCIA_INGRESO
   eCB_TRANSERENCIA_EGRESO
   eCB_DETRACCION_INGRESO
   eCB_DETRACCION_EGRESO
   eCB_REV_AUTOMATICO_SOLI_PAGADO
   eCB_PAGO_RETENCION_DIRECTO_DESDE_CXP
   eCB_PAGO_CAJA_CHICA
   eCB_EGRESO_REPOSICION_CAJA_CHICA
   eCB_INGRESO_REPOSICION_CAJA_CHICA
   eCB_REVERSO_PAGO_CAJA_CHICA
   eCB_REVERSO_EGRESO_REPOSICION_CAJA_CHICA
   eCB_REVERSO_INGRESO_REPOSICION_CAJA_CHICA
End Enum

Public Enum enum_MesesDelAno
    eMDA_ENERO = 1
    eMDA_FEBRERO
    eMDA_MARZO
    eMDA_ABRIL
    eMDA_MAYO
    eMDA_JUNIO
    eMDA_JULIO
    eMDA_AGOSTO
    eMDA_SEPTIEMBRE
    eMDA_OCTUBRE
    eMDA_NOVIEMBRE
    eMDA_DICIEMBRE
End Enum

Public Enum enum_TipoComision
   eTC_PorPorcentaje = 0
   eTC_PorMonto
End Enum

Enum enum_ComisionesEnFactura
   eCEF_SobreTotalFactura = 0
   eCEF_SobreRenglones
   eCEF_SobreTotalFacturayRenglones
End Enum

Enum enum_ComisionesEnRenglones
   eCER_PorUnVendedor = 0
   eCER_PordosVendedores
   eCER_PorTresVendedores
End Enum

Public Enum enum_ItemsMonto
   eIM_NO_PERMITIR_ITEMS_NEGATIVOS = 0
   eIM_PERMITIR_CON_AUTORIZACION
   eIM_PERMITIR_SIEMPRE
End Enum

Public Enum enum_Refinanciado
   eRe_ES_REFINANCIADO = 0
   eRe_SIN_REFINANCIAR
   eRe_PROVIENE_REFINANCIAR
End Enum

Public Enum enum_SalonDeCurso
   eSDC_SALA1 = 0
   eSDC_SALA2
   eSDC_SALA3
End Enum

Public Enum enum_ModoDeEnvioDeClave
   eMDEDC_NO_ASIGNADO = 0
   eMDEDC_INDETERMINADO '(para ser asignado en reestructuración)
   eMDEDC_TELEFONO
   eMDEDC_FAX
   eMDEDC_MAIL
   eMDEDC_MESSENGER
   eMDEDC_EN_PERSONA
End Enum

Public Enum enum_TipoArticuloInv
   eTAI_Simple = 0
   eTAI_UsaTalla_Color
   eTAI_UsaSerial
   eTAI_UsaTalla_ColorySerial
   eTAI_UsaSerial_Rollo
End Enum

Public Enum enum_AccionAlAnularFactDeMesesAnt
   eAAAFDMA_PermitirAnularSinChequear = 0
   eAAAFDMA_PreguntarSiDeseaAnular
   eAAAFDMA_NoPermitirAnular
End Enum

Public Enum enum_StatusTalonario
   eST_ACTIVA = 0
   eST_INACTIVA
End Enum

Public Enum enum_InformesConVentasDeTercero
   eIVT_TodasLasVentas = 0
   eIVT_VentasPropias
   eIVT_SoloVentasDeTercero
End Enum

Public Enum enum_ResumenCompra
   eRC_SoloCompras = 0
   eRC_CompraVentas
End Enum

Public Enum enum_ResumenVentas
   eRv_Soloventas = 0
   eRv_CompraVentas
End Enum

Public Enum enum_TalonarioAUsar
   eTaU_TALONARIO_UNICO = 0
   eTaU_MULTIPLES_TALONARIOS
End Enum

Public Enum enum_ConsultarDataPor
   eCDP_DESCUADRE = 0
   eCDP_ERROR_DE_SISTEMA
   eCDP_ERROR_DE_USUARIO
   eCDP_CONVERSION
   eCDP_INTEGRACION
   eCDP_CONVERSION_E_INTEGRACION
End Enum

Public Enum Enum_ImpresorasFiscales
   eIF_EPSON_PF_220 = 0
   eIF_EPSON_PF_300
   eIF_EPSON_TM_675_PF
   eIF_EPSON_TM_950_PF
   eIF_BEMATECH_MP_20_FI_II
   eIF_SAMSUNG_BIXOLON_SRP_270
   eIF_EPSON_PF_220II
   eIF_OKI_ML_1120
   eIF_SAMSUNG_BIXOLON_SRP_350
   eIF_ACLAS_PP1F3
   eIF_BMC_CAMEL
   eIF_BMC_SPARK_614
   eIF_BEMATECH_MP_2100_FI
   eIF_DASCOM_TALLY_1125
   eIF_QPRINT_MF
   eIF_ELEPOS_VMAX
   eIF_DASCOM_TALLY_DT_230
   eIF_SAMSUNG_BIXOLON_SRP_280
   eIF_ACLAS_PP9
   eIF_SAMSUNG_BIXOLON_SRP_812
   eif_HKA80
   eif_HKA112
   eIF_BMC_TH34_EJ
End Enum

Public Enum Enum_EstadoImpresionFiscal
   eEIF_VERIFICANDO_CONEXION_CON_IMPRESORA = 0
   eEIF_CIERRE_X
   eEIF_CIERRE_Z
   eEIF_OBTENER_SERIAL
   eEIF_IMPRIMIR_VENTA
   eEIF_IMPRIMIR_NOTA_DE_CREDITO
   eEIF_OBTENER_ULTIMO_NUMERO_FISCAL
   eEIF_CANCELA_CUPON_FISCAL
   eEIF_VERIFICA_ALICUOTA_VIGENTE
   eEIF_VER_STATUS
   eEIF_ACTIVA_REDONDEO
   eEIF_OBTENER_ULTIMO_REPORTE_Z
   eEIF_ASIGNAR_CONFIGURACION
   eEIF_CORTAR_PAPEL
End Enum

Public Enum enum_TipoDocumentoIdentificacionPeru
   eTDIP_RUC = 0
   eTDIP_DNI
   eTDIP_CARNET_DE_EXTRANJERIA
   eTDIP_PASAPORTE
   eTDIP_OTROS_TIPOS_DE_DOCUMENTOS
End Enum

Public Enum enum_TipoDeFormatoFecha
   eCSF_CON_BARRA = 0
   eCSF_CON_GUION
   eCSF_CON_PUNTO
   eCSF_CON_OTRO
End Enum

Public Enum enum_TipoDeFormaDeCobro
   eTFC_EFECTIVO = 0
   eTFC_TARJETA_DEBITO
   eTFC_TARJETA_CREDITO
   eTFC_CHEQUE
   eTFC_OTROS
End Enum

'iva
Enum enum_TipoProveedorIvaDeLibrosFiscales
   eTP_TP_CONRIF = 0
   eTP_TP_SINRIF
   eTP_TP_NORESIDENCIADO
   eTP_TP_NODOMICILIADO
End Enum

Public Enum enum_CampoCodigoAlternativoDeArticulo
   eCCADA_NoAsignado = 0
   eCCADA_CampoDefinible1
   eCCADA_CampoDefinible2
   eCCADA_CampoDefinible3
   eCCADA_CampoDefinible4
End Enum

Enum enum_StatusRetencionIva
   eSR_NORETENIDA = 0
   eSR_RETENIDA
End Enum
Enum enum_TipoDeVentaIva
   eTD_TV_VENTASEXENTAS = 0
   eTD_TV_VENTASEXPORTACION
   eTD_TV_VENTASCONTRIBUYENTES
   eTD_TV_VENTASNOCONTRIBUYENTES
   eTD_TV_VENTASPTOLIBRENE
   eTD_TV_VENTASNOCONTRIBPTOLIBRENE
   eTD_TV_VENTASEXONERADAS
   eTD_TV_VENTASNOSUJETAS
   eTD_TV_VENTASSINDERECHOACREDITO
End Enum
Enum enum_StatusVentaIva
   eSV_COBRADA = 0
   eSV_PORCOBRAR
End Enum

Enum enum_StatusCompraIva
   eSC_PAGADA = 0
   eSC_PORPAGAR
End Enum
    
Enum enum_ReporteRPTIVA
   eRPT_RelacionDeCompras = 0
   eRPT_RelacionDeVentas
   eRPT_InformeDeCompraVentas
   eRPT_InfDeCompraOVentaAUnClientOPro
   eRPT_InfCreditosDebitosEnUnMes
   eRPT_InfCredDebEnUnMesResumido
   eRPT_InfCredDebDescuadrados
   eRPT_PlanillaH99Numero294638
   eRPT_PlanillaH99Numero0070556
   eRPT_PlanillaH2000Numero0015483
   eRPT_Forma30
   eRPT_BorradorPlanillaForma30
   eRPT_InformeDeProrrateo
   eRPT_Forma0030
   eRPT_LibroDeCompra
   eRPT_LibroDeCompraXLS
   eRPT_RetencionesDeIvaCompraVenta
   eRPT_InformeDeProrrateoNuevoFormato
   eRPT_SubRepLibroDeCompraXLS
   eRPT_LibroDeVenta
   eRPT_LibroDeVentaXLS
   eRPT_SubRepLibroDeVentasXLS
   eRPT_Forma0030olivenca
   eRPT_PruebaDeImpresion
   eRPT_ComprobanteRetencionDeIva
   eRPT_ComprobanteResumen
   eRPT_ComprobantesAutomaticosModificados
   eRPT_DocumentosSinComprobantes
   eRPT_DocumentosContabilizados
   eRPT_ComprobantesCompraVentaDescuadrados
   eRPT_ComprobantesCompraVentaPorLoteDescuadrados
   eRPT_LibroDeVentaApaisado
   eRPT_ResumenDeComprobantes
   eRPT_ResumenDeCreditosYDebitosDelPeriodo
   eRPT_ResumenDeCreditosYDebitosDelPeriodoApaisado
   eRPT_subRelacionDeVentas
   eRPT_LibroDeVentaTerceros
   eRPT_LibroDeVentaApaisadoTerceros
   eRPT_informeClientesProveedores
   eRPT_informeIngresosBrutos
   eRPT_ComprobanteResumenApaisado
   eRPT_ComprobanteResumen2006
   eRPT_InformeRifRepetidoClienteProveedor
   eRPT_InformeVentasPorTalonario
   eRPT_ReporteDeInconsistenciasEnPago
End Enum
'iva

Public Enum enum_AccionLimiteItemsFactura
   eALIF_SOLO_ADVERTIR = 0
   eALIF_NO_EXEDER_NUMERO_DE_ITEMS
End Enum

Public Enum enum_TipoDePlanillaImprimir
   eTDPI_FORMA_00030_Nueva_DelCentro_Casillas_Separadas_F2006 = 0
   eTDPI_FORMA_00030_Nueva_Grafibond
   eTDPI_FORMA_00030_GRAFICHECK_Casillas_Separadas_F2007
   eTDPI_FORMA_00030_Nueva_DelCentro_Casillas_Separadas_F2008
End Enum
Public Enum enum_HojasDeLaPlanilla
   eHDLP_PrimeraHoja = 0
   eHDLP_SegundaHoja
   eHDLP_AmbasHojas
End Enum

Public Enum enum_MonedasVenezolanas
   eMV_Bolivar = 0
   eMV_BolivarFuerte
End Enum
Public Enum enum_TipoDeOperacionSeniat
   eTO_NO_APLICA = 0
   eTO_DIVIDENDO_ACCIONES
   eTO_DIVIDENDO_EFECTIVO
   eTO_VENTAS_ACCIONES
End Enum

Public Enum enum_TipoNegocio
   eTN_General = 0
   eTN_TallerMecanico
End Enum

Public Enum enum_StatusNotaEntradaSalida
   eSNES_Vigente = 0
   eSNES_Anulada
End Enum
Public Enum enum_TipoDeMetodoDeCosteo
   eTDMDC_UltimoCosto = 0
   eTDMDC_CostoPromedio
End Enum
Public Enum enum_AgrupacionParaElLibroDeVentas
   eAPLV_PORRESUMENDEVENTAS = 0
   eAPLV_PORTIPODECONTRIBUYENTE
   eAPLV_DETALLADOPORFACTURA
End Enum

Public Enum enum_CampoAMostrarCodigoRIF
   eCAMCR_CODIGO = 0
   eCAMCR_RIF
End Enum
Public Enum enum_CantidadDeDecimales
   eCDD_2 = 0
   eCDD_3
   eCDD_4
End Enum

Public Enum enum_SolicitudGeneradaPor
   eSGP_CalculosDeNomina = 0
   eSGP_Vacaciones
   eSGP_Utilidades
   eSGP_PagoDeIntereses
   eSGP_Fideicomiso
   eSGP_Liquidacion
   eSGP_AdelantoDeVacaciones
   eSGP_AnticipoDePrestaciones
   eSGP_NoAplica
End Enum
 
Public Enum enum_StatusSolicitud
   eSS_PorProcesar = 0
   eSS_Procesada
   eSS_ParcialmenteProcesada
   eSS_Anulada
End Enum

Public Enum enum_StatusSolicitudRenglon
   eSSR_PorProcesar = 0
   eSSR_Procesada
End Enum

Public Enum enum_TipoDeFormaDePagoSolicitud
   eTDFDPS_Efectivo = 0
   eTDFDPS_transferencia
   eTDFDPS_Cheque
End Enum
 

Public Enum enum_OrigenCXPoCXCoManual
   eOCXPCXCM_CxP = 0
   eOCXPCXCM_CXC
   eOCXPCXCM_Manual
End Enum
Public Enum enum_Origen
   eO_Pago = 0
   eO_cxP
End Enum
Public Enum enum_TipoDeTransaccionMunicipal
   eTDTM_NoAplica = 0
   eTDTM_Compra
   eTDTM_Servicio
End Enum
Public Enum enum_TipoConexion
   eTDC_PUERTO_SERIAL = 0
   eTDC_USB
   eTDC_LAN
End Enum

Public Enum enum_StatusRendicion
   eSRd_EN_PROCESO = 0
   eSRd_CERRADA
   eSRd_ANULADA
End Enum
Public Enum enum_OrigenRendicion
   eOR_Manual = 0
   eOR_Sistema10
End Enum
Public Enum enum_TipoDeDocumentoRendicion
   eTDDR_Rendicion = 0
   eTDDR_Reposicion
End Enum

'Public Enum enum_TipoDeCompraEspecial
'   eTDCE_NoEs = 0
'   eTDCE_Informativa
'   eTDCE_SueldosySalarios
'End Enum

Public Enum enum_StatusControlDespacho
   eSCD_PorProcesar = 0
   eSCD_Procesada
   eSCD_Anulada
End Enum

Public Enum enum_BloquearEmision
   eBE_NoBloquear = 0
   eBE_BloquearPorCaja
   eBE_BloqueoGeneral
End Enum

Public Enum enum_TipoDeDocumentoCobranza
   eTDDC_CobranzaDeFactura = 0
   eTDDC_CobranzaPorAplicacionDeRetencion
   eTDDC_CobranzaPorRefinanciamiento
End Enum

 Public Enum enum_AplicacionAlicuota
        eAA_NoAplica = 0
        eAA_SustituirAlicuotaReducida
        eAA_SustituirAlicuotaAdicional
End Enum

Public Enum enum_FechaSugeridaRetencionCxP
   eFSRCP_FechaFacturaCxP = 0
   eFSRCP_FechaRegistroCxP
End Enum

Public Enum enum_EstatusAlicuotaImpuestoEspecial
   eEAIE_Activo = 0
   eEAIE_Inactivo
End Enum

Public Enum enum_AplicaAContribuyente
   eAAC_TodosLosContribuyentes = 0
   eAAC_SoloPersonasNaturales
   eAAC_SoloPersonasJuridicas
End Enum

Public Enum enum_AlicuotaIVA
   eAIVA_Reducida = 0
   eAIVA_Extendida
End Enum

Public Enum enum_BaseCalculoParaAlicuotaEspecial
   eBCPAE_TotalFactura = 0
   eBCPAE_MontoExentoMasBaseImponible
   eBCPAE_SoloBaseImponible
   eBCPAE_SoloBaseImponibleAlicuotaGeneral
End Enum

Enum enum_TipoDeAlicuotaEspecial
   eTDEsp_No = 0
   eTDEsp_1
   eTDEsp_2
End Enum

Public Enum enum_StatusCompania
   eSCom_Activa = 0
   eSCom_Inactiva
End Enum

Public Enum enum_TipoDePersonaLibrosElectronicos
   eTDPLE_NaturalDomiciliado = 0
   eTDPLE_JuridicoDomiciliado
   eTDPLE_NaturalNoDomiciliado
   eTDPLE_JuridicoNoDomiciliado
End Enum

Public Enum enum_TipoDeCambioSunat
   eTDCS_COMPRA = 0
   eTDCS_VENTA
End Enum

Public Enum enum_TipoDeMercancia
   eTDM_NoAplica = 0
   eTDM_MateriaPrima
   eTDM_ProductoTerminado
End Enum


Public Enum enum_TipoDeMoneda
   eTDMON_FISICA = 0
   eTDMON_CRIPTOACTIVO
   eTDMON_OTRAS
End Enum


Public Enum enum_TipoGeneradoPorNotaDeEntradaSalida
   eTGPNDES_Usuario = 0
   eTGPNDES_OrdenDeProduccion
End Enum

Public Enum enum_TipoNotaProduccion
   eTNP_NoAplica = 0
   eTNP_Abrir
   eTNP_Cerrar
   eTNP_Ajuste
End Enum


Public Function me_saw() As Long
   me_saw = 1
End Function

Public Function me_WinCont() As Long
   me_WinCont = 2
End Function

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsEnumAdministrativo"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Enumerativos del Administrativo"
End Function

Public Function enum_SystemModulesToStr(module As enum_SystemModules) As String
   Select Case module
      Case eSM_MenuG: enum_SystemModulesToStr = "G"
      Case eSM_AlicuotaIVA: enum_SystemModulesToStr = "Alicuota " & gGlobalization.fPromptIVA
      Case eSM_Almacen: enum_SystemModulesToStr = "Almacen"
      Case eSM_ArticuloInventario: enum_SystemModulesToStr = "Artículo Inventario"
      Case eSM_Autorizacion: enum_SystemModulesToStr = "Autorizacion"
      Case eSM_Banco: enum_SystemModulesToStr = "Banco"
      Case eSM_Caja: enum_SystemModulesToStr = "Caja Registradora"
      Case eSM_CajaApertura: enum_SystemModulesToStr = "Caja Apertura"
      Case eSM_CambiarDireccionDeDespacho: enum_SystemModulesToStr = "Cambiar Direccion De Despacho"
      Case eSM_CamposDefCotizacion: enum_SystemModulesToStr = "Campos Def Cotizacion"
      Case eSM_CamposDefFactura: enum_SystemModulesToStr = "Campos Def Factura"
      Case eSM_CambioaMonedaLocal: enum_SystemModulesToStr = "Cambio a Moneda Local"
      Case eSM_CamposMonedaExtranjera: enum_SystemModulesToStr = "Campos Moneda Extranjera"
      Case eSM_Categoria: enum_SystemModulesToStr = "Categoria"
      Case eSM_CategoriaCliente: enum_SystemModulesToStr = "Categoría Cliente"
      Case eSM_CategoriaProveedor: enum_SystemModulesToStr = "Categoría Proveedor"
      Case eSM_CIIU: enum_SystemModulesToStr = "C.I.I.U."
      Case eSM_Ciudad: enum_SystemModulesToStr = "Ciudad"
      Case eSM_ClaveSuperUtilitario: enum_SystemModulesToStr = "Clave Super Utilitario"
      Case eSM_Cliente: enum_SystemModulesToStr = "Cliente"
      Case eSM_Cobranza: enum_SystemModulesToStr = "Cobranza"
      Case eSM_Color: enum_SystemModulesToStr = "Color"
      Case eSM_ComisionCobranzaXVcto: enum_SystemModulesToStr = "Comisión Cobranza por Vencimiento"
      Case eSM_Compania: enum_SystemModulesToStr = "Empresa"
      Case eSM_Compra: enum_SystemModulesToStr = "Compra"
      Case eSM_ConceptoBancario: enum_SystemModulesToStr = "Concepto Bancario"
      Case eSM_Conciliacion: enum_SystemModulesToStr = "Conciliacion"
      Case eSM_ConteoFisico: enum_SystemModulesToStr = "Conteo Físico"
      Case eSM_Contrato: enum_SystemModulesToStr = "Contrato"
      Case eSM_CorreccionDeFechas: enum_SystemModulesToStr = "Corrección de fechas"
      Case eSM_Cotizacion: enum_SystemModulesToStr = "Cotizacion"
      Case eSM_CuentaBancaria: enum_SystemModulesToStr = "Cuenta Bancaria"
      Case eSM_Curso: enum_SystemModulesToStr = "Curso"
      Case eSM_CxC: enum_SystemModulesToStr = "CxC"
      Case eSM_CxP: enum_SystemModulesToStr = "CxP"
      Case eSM_Despacho: enum_SystemModulesToStr = "Despacho"
      Case eSM_DiaDeDeclaracionDelIVA: enum_SystemModulesToStr = "Día de Declaración del " & gGlobalization.fPromptIVA
      Case eSM_DireccionDeDespacho: enum_SystemModulesToStr = "Direccion De Despacho"
      Case eSM_DocumentoCobrado: enum_SystemModulesToStr = "Documento Cobrado"
      Case eSM_ExistenciaPorAlmacen: enum_SystemModulesToStr = "Existencia Por Almacen"
      Case eSM_ExistenciaPorGrupo: enum_SystemModulesToStr = "Existencia Por Grupo"
      Case eSM_Factura: enum_SystemModulesToStr = "Factura"
      Case eSM_FacturasVendedor: enum_SystemModulesToStr = "Facturas Vendedor"
      Case eSM_FormaDelCobro: enum_SystemModulesToStr = "Forma de Cobro"
      Case eSM_GeneracionDeContratos: enum_SystemModulesToStr = "Generacion De Contratos"
      Case eSM_GrupoTallaColor: enum_SystemModulesToStr = "Grupos de Tallas y Colores"
      Case eSM_LineaDeProducto: enum_SystemModulesToStr = "Linea De Producto"
      Case eSM_Llamada: enum_SystemModulesToStr = "Llamada"
      Case eSM_MailFax: enum_SystemModulesToStr = "Mail/Fax"
      Case eSM_MaquinaFiscal: enum_SystemModulesToStr = "Máquina Fiscal"
      Case eSM_MarcaVehiculo: enum_SystemModulesToStr = "Marca Vehículo"
      Case eSM_ModeloVehiculo: enum_SystemModulesToStr = "Modelo Vehículo"
      Case eSM_Moneda: enum_SystemModulesToStr = "Moneda"
      Case eSM_Moneda: enum_SystemModulesToStr = "Moneda Local"
      Case eSM_MovimientoBancario: enum_SystemModulesToStr = "Movimiento Bancario"
      Case eSM_NotaDeEntradaSalida: enum_SystemModulesToStr = "Nota de Entrada/Salida/Auto-Consumos/Retiros"
      Case eSM_NotaFinal: enum_SystemModulesToStr = "Nota Final"
      Case eSM_OPFalsoRetencion: enum_SystemModulesToStr = "Op Falso Retención"
      Case eSM_OrdenDeCompra: enum_SystemModulesToStr = "Orden de Compra"
      Case eSM_OrdenDeServicio: enum_SystemModulesToStr = "Orden de Servicio"
      Case eSM_OtrosCargosDeFactura: enum_SystemModulesToStr = "Otros Cargos de Factura"
      Case eSM_Parametros: enum_SystemModulesToStr = "Parámetros Administrativo"
      Case eSM_Pago: enum_SystemModulesToStr = "Pago"
      Case eSM_Pais: enum_SystemModulesToStr = "Pais"
      Case eSM_Parametros: enum_SystemModulesToStr = "Parametros"
      Case eSM_ParametrosCompania: enum_SystemModulesToStr = "Parámetros Compañía"
      Case eSM_PlanillaForma00030: enum_SystemModulesToStr = "Planilla Forma 00030"
      Case eSM_PreguntasMasComunes: enum_SystemModulesToStr = "Preguntas"
      Case eSM_ProductoCompuesto: enum_SystemModulesToStr = "Producto Compuesto"
      Case eSM_PropAnalisisVenc: enum_SystemModulesToStr = "Prop Analisis Venc"
      Case eSM_Proveedor: enum_SystemModulesToStr = "Proveedor"
      Case eSM_ReglasDeContabilizacion: enum_SystemModulesToStr = "Reglas de Contabilización"
      Case eSM_RenglonArtCompuestoCot: enum_SystemModulesToStr = "Renglon Artículos Compuesto en Cotización"
      Case eSM_RenglonArtCompuestoFac: enum_SystemModulesToStr = "Renglon Artículos Compuesto en Factura"
      Case eSM_RenglonCobroDeFactura: enum_SystemModulesToStr = "Renglón Cobro de Factura"
      Case eSM_RenglonComisionesDeVendedor: enum_SystemModulesToStr = "Renglón Comisiones de Vendedor"
      Case eSM_RenglonCompra: enum_SystemModulesToStr = "Renglón Compra"
      Case eSM_RenglonCompraXSerial: enum_SystemModulesToStr = "Renglón Compra por Serial"
      Case eSM_RenglonConteoFisico: enum_SystemModulesToStr = "Renglón Conteo Físico"
      Case eSM_RenglonContrato: enum_SystemModulesToStr = "Renglón Contrato"
      Case eSM_RenglonCotizacion: enum_SystemModulesToStr = "Renglón Cotizacion"
      Case eSM_RenglonExistenciaAlmacen: enum_SystemModulesToStr = "Renglon Existencia por Almacen"
      Case eSM_RenglonFactura: enum_SystemModulesToStr = "Renglón Factura"
      Case eSM_RenglonGrupoColor: enum_SystemModulesToStr = "Renglón Grupo Color"
      Case eSM_RenglonGrupoTalla: enum_SystemModulesToStr = "Renglón Grupo Talla"
      Case eSM_RenglonNotaES: enum_SystemModulesToStr = "Renglón Nota Entrada / Salida"
      Case eSM_RenglonOtrosCargosDeCotizacion: enum_SystemModulesToStr = "Renglón Otros Cargos de Cotización"
      Case eSM_RenglonOtrosCargosDeFactura: enum_SystemModulesToStr = "Renglón Otros Cargos de Factura"
      Case eSM_RenglonTransferencia: enum_SystemModulesToStr = "Renglón Transferencia"
      Case eSM_RespaldarRestaurar: enum_SystemModulesToStr = "Respaldar / Restaurar"
      Case eSM_ResumenDiarioVentas: enum_SystemModulesToStr = "Resumen Diario"
      Case eSM_RetDocumentoPagado: enum_SystemModulesToStr = "Ret Documento Pagado"
      Case eSM_RetirosACuenta: enum_SystemModulesToStr = "Retiros a Cuenta"
      Case eSM_RetPago: enum_SystemModulesToStr = "Ret Pago"
      Case eSM_RevisionDeData: enum_SystemModulesToStr = "Revision De Data"
      Case eSM_SectorDeNegocio: enum_SystemModulesToStr = "Sector de Negocio"
      Case eSM_TablaRetencion: enum_SystemModulesToStr = "Tabla Retencion"
      Case eSM_Talla: enum_SystemModulesToStr = "Talla"
      Case eSM_TipoDeCurso: enum_SystemModulesToStr = "Tipo De Curso"
      Case eSM_TipoProveedor: enum_SystemModulesToStr = "Tipo Proveedor"
      Case eSM_Transferencia: enum_SystemModulesToStr = "Transferencia"
      Case eSM_UnidadDeVenta: enum_SystemModulesToStr = "Unidad De Venta"
      Case eSM_UrbanizacionZP: enum_SystemModulesToStr = "Urbanización ZP"
      Case eSM_Usuario: enum_SystemModulesToStr = "Usuario"
      Case eSM_Vendedor: enum_SystemModulesToStr = "Vendedor"
      Case eSM_VersionPrograma: enum_SystemModulesToStr = "Versión Programa"
      Case eSM_Visita: enum_SystemModulesToStr = "Visita"
      Case eSM_ZonaCobranza: enum_SystemModulesToStr = "Zona Cobranza"
      Case eSM_ProductoCompuesto: enum_SystemModulesToStr = "Producto Compuesto"
      Case eSM_GestionDeCobranza:  enum_SystemModulesToStr = "Gestión de Cobranza"
      Case eSM_ComprobanteRetencionIVA: enum_SystemModulesToStr = "Comprobante Retención " & gGlobalization.fPromptIVA
      Case eSM_ChequeDeMovBancario: enum_SystemModulesToStr = "Cheque de Mov. Bancario"
      Case eSM_ServiciosAlRealizarVisita: enum_SystemModulesToStr = "Servicios Al Realizar Visita"
      Case eSM_PlanillaForma00030Nueva: enum_SystemModulesToStr = "Planilla Forma 00030 Nueva"
      Case eSM_PDT: enum_SystemModulesToStr = "Planillas Declarativas (PDT)"
      Case eSM_Rendicion: enum_SystemModulesToStr = "Rendición"
      Case eSM_CajaChica: enum_SystemModulesToStr = "Reposicion de Caja Chica"
      Case eSM_Adelanto: enum_SystemModulesToStr = "Adelanto"
      Case eSM_TipoDeDocumentoLey: enum_SystemModulesToStr = "Tipo de Documento de Ley"
      Case eSM_Detracciones: enum_SystemModulesToStr = "Detracciones "
      Case eSM_CierreCostoPeriodo: enum_SystemModulesToStr = "Costo Promedio"
      Case esM_DetalleDeConciliacion: enum_SystemModulesToStr = "Detalle de Conciliacion"
      Case esM_InteligenciaDeNegocio: enum_SystemModulesToStr = "Inteligencia De Negocio"
      Case esM_InformesVendedores: enum_SystemModulesToStr = "Informes Vendedores"
 
      Case eSM_Vehiculo: enum_SystemModulesToStr = "Vehículo"
      Case eSM_ControlDespacho: enum_SystemModulesToStr = "Control Despacho"
      Case eSM_DetalleDeControlDespacho: enum_SystemModulesToStr = "Detalle de Control Despacho"
 
      Case eSM_Beneficiario: enum_SystemModulesToStr = "Beneficiario"
      Case eSM_SolicitudesDePago: enum_SystemModulesToStr = "Solicitudes De Pago"
      Case eSM_RenglonSolicitudesDePago: enum_SystemModulesToStr = "Renglon Solicitudes De Pago"
      Case eSM_Integracion: enum_SystemModulesToStr = "Integracion"
      Case eSM_Municipio: enum_SystemModulesToStr = "Municipio"
      Case eSM_MunicipioCiudad: enum_SystemModulesToStr = "MunicipioCiudad"
      Case eSM_ValorUT: enum_SystemModulesToStr = "Valor De UT"
      Case eSM_ClasificadorActividadEconomica: enum_SystemModulesToStr = "Clasificador Actividad Economica"
      Case eSM_ImpuestoMunicipalRetNavigator: enum_SystemModulesToStr = "ImpuestoMunicipal Ret "
      Case eSM_RenglonImpuestoMunicipalRetNav: enum_SystemModulesToStr = "RenglonImpuesto Municipal Ret"
      Case eSM_NotaDeEntrega: enum_SystemModulesToStr = "Nota de Entrega"
      Case eSM_CompraDetalleGasto: enum_SystemModulesToStr = "Compra Detalle Gasto"
      'módulos de winret
      Case eSM_PlanillaDec: enum_SystemModulesToStr = "Planilla de Declaración"
      Case eSM_PlantillaRET: enum_SystemModulesToStr = "Plantilla de Retenciones"
      Case eSM_ValorUT: enum_SystemModulesToStr = "Valor U.T."
      Case eSM_TarifaN2: enum_SystemModulesToStr = "Tarifa N°2"
      Case eSM_ARCV: enum_SystemModulesToStr = "ARCV"
      Case eSM_RelacionAnual: enum_SystemModulesToStr = "Relación Anual"
      Case eSM_RenglonARCV: enum_SystemModulesToStr = "Renglón ARCV"
      Case eSM_ConversionContabilidad: enum_SystemModulesToStr = "Conversión"
      Case eSM_Contabilizar: enum_SystemModulesToStr = "Contabilizar"
      Case eSM_Balances: enum_SystemModulesToStr = "Balances"
      Case eSM_Informes: enum_SystemModulesToStr = "Informes"
      Case eSM_ImportarExportar: enum_SystemModulesToStr = "Importar/Exportar"
      Case eSM_CerrarPeriodo: enum_SystemModulesToStr = "Cerrar Periodo"
      Case eSM_LoteAdm: enum_SystemModulesToStr = "Lote Administrativo"
      Case eSM_Anticipo: enum_SystemModulesToStr = "Anticipo"
      Case eSM_OrdenDeCompraDetalleArticuloInventario: enum_SystemModulesToStr = "Orden De Compra Detalle Articulo Inventario"
      Case eSM_CondicionesDePago: enum_SystemModulesToStr = "Condiciones De Pago"
      Case eSM_ListaDeMateriales: enum_SystemModulesToStr = "Lista De Materiales"
      Case eSM_ListaDeMaterialesDetalleArticulo: enum_SystemModulesToStr = "Lista De Materiales Detalle Articulo"
      Case eSM_OrdenDeProduccion: enum_SystemModulesToStr = "Orden de Producción"
      Case eSM_OrdenDeProduccionDetalleArticulo: enum_SystemModulesToStr = "Orden de Producción Detalle Articulo"
      Case eSM_OrdenDeProduccionDetalleMateriales: enum_SystemModulesToStr = "Orden de Producción Detalle Materiales"
   End Select
End Function

Public Function enumDiaDeLaSemanaToString(DiaDeLaSemana As enum_DiaDeLaSemana) As String
   Select Case DiaDeLaSemana
      Case eDD_LUNES: enumDiaDeLaSemanaToString = "Lunes"
      Case eDD_MARTES: enumDiaDeLaSemanaToString = "Martes"
      Case eDD_MIERCOLES: enumDiaDeLaSemanaToString = "Miércoles"
      Case eDD_JUEVES: enumDiaDeLaSemanaToString = "Jueves"
      Case eDD_VIERNES: enumDiaDeLaSemanaToString = "Viernes"
      Case eDD_SABADO: enumDiaDeLaSemanaToString = "Sábado"
      Case eDD_DOMINGO: enumDiaDeLaSemanaToString = "Domingo"
      Case Else: enumDiaDeLaSemanaToString = enumDiaDeLaSemanaToString(0)
   End Select
End Function

Public Function strDiaDeLaSemanaToNum(ByVal DiaDeLaSemana As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_DiaDeLaSemana
   Dim nCount As Integer
   nCount = 0
   For mEnum = eDD_DOMINGO To eDD_SABADO
   If UCase(DiaDeLaSemana) = UCase(enumDiaDeLaSemanaToString(mEnum)) Then
      strDiaDeLaSemanaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strDiaDeLaSemanaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & DiaDeLaSemana & " no existe en tipo enumerativo diaDeLaSemana"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithDiaDeLaSemana(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_LUNES)
      End If
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_LUNES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_MARTES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_MIERCOLES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_JUEVES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_VIERNES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_SABADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDiaDeLaSemanaToString(enum_DiaDeLaSemana.eDD_DOMINGO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusVendedorToString(ByVal statusVendedor As enum_StatusVendedor) As String
   Select Case statusVendedor
      Case eSV_ACTIVO: enumStatusVendedorToString = "Activo"
      Case eSV_INACTIVO: enumStatusVendedorToString = "Inactivo"
      Case eSV_RESTRINGIDO: enumStatusVendedorToString = "Restringido"
      Case Else: enumStatusVendedorToString = enumStatusVendedorToString(0)
   End Select
End Function

Public Function strStatusVendedorToNum(ByVal statusVendedor As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusVendedor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSV_ACTIVO To eSV_RESTRINGIDO
   If UCase(statusVendedor) = UCase(enumStatusVendedorToString(mEnum)) Then
      strStatusVendedorToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusVendedorToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusVendedor & " no existe en tipo enumerativo statusVendedor"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusVendedor(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusVendedorToString(enum_StatusVendedor.eSV_ACTIVO)
      End If
      varComboBox.List(nCount) = enumStatusVendedorToString(enum_StatusVendedor.eSV_ACTIVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusVendedorToString(enum_StatusVendedor.eSV_INACTIVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusVendedorToString(enum_StatusVendedor.eSV_RESTRINGIDO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeContribuyenteToString(ByVal tipoDeContribuyente As enum_TipoDeContribuyente) As String
   Select Case tipoDeContribuyente
      Case eTD_CONTRIBUYENTE: enumTipoDeContribuyenteToString = "Contribuyente"
      Case eTD_NOCONTRIBUYENTE: enumTipoDeContribuyenteToString = "No Contribuyente"
      Case Else: enumTipoDeContribuyenteToString = enumTipoDeContribuyenteToString(0)
   End Select
End Function
Public Function enumTipoDeContribuyenteToSiglas(ByVal tipoDeContribuyente As enum_TipoDeContribuyente) As String
   Select Case tipoDeContribuyente
      Case eTD_CONTRIBUYENTE: enumTipoDeContribuyenteToSiglas = "CON"
      Case eTD_NOCONTRIBUYENTE: enumTipoDeContribuyenteToSiglas = "N/C"
      Case Else: enumTipoDeContribuyenteToSiglas = enumTipoDeContribuyenteToSiglas(0)
   End Select
End Function
Public Function strTipoDeContribuyenteToNum(ByVal tipoDeContribuyente As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeContribuyente
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_CONTRIBUYENTE To eTD_NOCONTRIBUYENTE
   If UCase(tipoDeContribuyente) = UCase(enumTipoDeContribuyenteToString(mEnum)) Then
      strTipoDeContribuyenteToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeContribuyenteToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeContribuyente & " no existe en tipo enumerativo tipoDeContribuyente"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeContribuyente(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeContribuyenteToString(enum_TipoDeContribuyente.eTD_CONTRIBUYENTE)
      End If
      varComboBox.List(nCount) = enumTipoDeContribuyenteToString(enum_TipoDeContribuyente.eTD_CONTRIBUYENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeContribuyenteToString(enum_TipoDeContribuyente.eTD_NOCONTRIBUYENTE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDePersonaFiscalToString(ByVal tipoDePersonaFiscal As enum_TipoDePersonaFiscal) As String
   Select Case tipoDePersonaFiscal
      Case eTD_PERSONAJURIDICA: enumTipoDePersonaFiscalToString = "Persona Jurídica"
      Case eTD_PERSONANATURAL: enumTipoDePersonaFiscalToString = "Persona Natural"
      Case Else: enumTipoDePersonaFiscalToString = enumTipoDePersonaFiscalToString(0)
   End Select
End Function

Public Function strTipoDePersonaFiscalToNum(ByVal tipoDePersonaFiscal As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDePersonaFiscal
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_PERSONAJURIDICA To eTD_PERSONANATURAL
   If UCase(tipoDePersonaFiscal) = UCase(enumTipoDePersonaFiscalToString(mEnum)) Then
      strTipoDePersonaFiscalToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDePersonaFiscalToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDePersonaFiscal & " no existe en tipo enumerativo tipoDePersonaFiscal"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDePersonaFiscal(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDePersonaFiscalToString(enum_TipoDePersonaFiscal.eTD_PERSONAJURIDICA)
      End If
      varComboBox.List(nCount) = enumTipoDePersonaFiscalToString(enum_TipoDePersonaFiscal.eTD_PERSONAJURIDICA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDePersonaFiscalToString(enum_TipoDePersonaFiscal.eTD_PERSONANATURAL)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusClienteToString(ByVal statusCliente As enum_StatusCliente) As String
   Select Case statusCliente
      Case eSC_ACTIVO: enumStatusClienteToString = "Activo"
      Case eSC_INACTIVO: enumStatusClienteToString = "Inactivo"
      Case eSC_RESTRINGIDO: enumStatusClienteToString = "Restringido"
      Case eSC_SUSPENDIDO: enumStatusClienteToString = "Suspendido"
      Case eSC_TIEMPO_SIN_CONTACTO: enumStatusClienteToString = "Mucho Tiempo sin contacto"
      Case eSC_DATOS_DESACTUALIZADOS: enumStatusClienteToString = "Datos Desactualizados"
      Case Else: enumStatusClienteToString = enumStatusClienteToString(0)
   End Select
End Function

Public Function strStatusClienteToNum(ByVal statusCliente As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCliente
   Dim nCount As Integer
   nCount = 0
      For mEnum = eSC_ACTIVO To eSC_DATOS_DESACTUALIZADOS
         If UCase(statusCliente) = UCase(enumStatusClienteToString(mEnum)) Then
            strStatusClienteToNum = nCount
            GoTo h_EXIT
         End If
         nCount = nCount + 1
      Next
   strStatusClienteToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusCliente & " no existe en tipo enumerativo statusCliente"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCliente(ByRef varComboBox As Object, Optional ByVal action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusClienteToString(enum_StatusCliente.eSC_ACTIVO)
      End If
      varComboBox.List(nCount) = enumStatusClienteToString(enum_StatusCliente.eSC_ACTIVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusClienteToString(enum_StatusCliente.eSC_INACTIVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusClienteToString(enum_StatusCliente.eSC_RESTRINGIDO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusClienteToString(enum_StatusCliente.eSC_SUSPENDIDO)
      If GetEsSistemaAdmInterno Then
         nCount = nCount + 1
         varComboBox.List(nCount) = enumStatusClienteToString(enum_StatusCliente.eSC_TIEMPO_SIN_CONTACTO)
         nCount = nCount + 1
         varComboBox.List(nCount) = enumStatusClienteToString(enum_StatusCliente.eSC_DATOS_DESACTUALIZADOS)
      End If
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusDocumentoToString(ByVal statusDocumento As enum_StatusDocumento) As String
   Select Case statusDocumento
      Case eSD_PORCANCELAR: enumStatusDocumentoToString = "Por Cancelar"
      Case eSD_CANCELADO: enumStatusDocumentoToString = "Cancelado"
      Case eSD_CHEQUEDEVUELTO: enumStatusDocumentoToString = "Cheque Devuelto"
      Case eSD_ABONADO: enumStatusDocumentoToString = "Abonado"
      Case eSD_ANULADO: enumStatusDocumentoToString = "Anulado"
      Case eSD_REFINANCIADO: enumStatusDocumentoToString = "Refinanciado"
      Case Else: enumStatusDocumentoToString = enumStatusDocumentoToString(0)
   End Select
End Function

Public Function strStatusDocumentoToNum(ByVal statusDocumento As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusDocumento
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSD_PORCANCELAR To eSD_REFINANCIADO
   If UCase(statusDocumento) = UCase(enumStatusDocumentoToString(mEnum)) Then
      strStatusDocumentoToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusDocumentoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusDocumento & " no existe en tipo enumerativo statusDocumento"
   End If
h_EXIT:
   Exit Function
End Function

Public Function strStatusDocumentoToNumToSiglas(ByVal statusDocumento As String) As Integer
   Dim mEnum As enum_StatusDocumento
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSD_PORCANCELAR To eSD_REFINANCIADO
   If UCase(statusDocumento) = UCase(enumStatusDocumentoToSiglas(mEnum)) Then
      strStatusDocumentoToNumToSiglas = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusDocumentoToNumToSiglas = 0
h_EXIT:
   Exit Function
End Function
Public Function strStatusDocumentoToSiglasToString(ByVal statusDocumento As String) As String
   Dim mEnum As enum_StatusDocumento
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSD_PORCANCELAR To eSD_REFINANCIADO
      If UCase(statusDocumento) = UCase(enumStatusDocumentoToSiglas(mEnum)) Then
         strStatusDocumentoToSiglasToString = enumStatusDocumentoToString(mEnum)
         GoTo h_EXIT
      End If
   Next
   strStatusDocumentoToSiglasToString = enumStatusDocumentoToString(eSD_PORCANCELAR)
h_EXIT:
   Exit Function
End Function
Public Function strStatusDocumentoToNumBoolean(ByVal statusDocumento As String) As Boolean
   Dim esUnStatus As Boolean
   Dim mEnum As enum_StatusDocumento
   esUnStatus = False
   For mEnum = eSD_PORCANCELAR To eSD_REFINANCIADO
   If UCase(statusDocumento) = UCase(enumStatusDocumentoToSiglas(mEnum)) Then
      esUnStatus = True
      GoTo h_EXIT
   End If
   Next
h_EXIT:
   strStatusDocumentoToNumBoolean = esUnStatus
   Exit Function
End Function

Public Function enumStatusDocumentoToSiglas(statusDocumento As enum_StatusDocumento) As String
   Select Case statusDocumento
      Case eSD_PORCANCELAR: enumStatusDocumentoToSiglas = "P/C"
      Case eSD_CANCELADO: enumStatusDocumentoToSiglas = "CAN"
      Case eSD_CHEQUEDEVUELTO: enumStatusDocumentoToSiglas = "C/D"
      Case eSD_ABONADO: enumStatusDocumentoToSiglas = "ABO"
      Case eSD_ANULADO: enumStatusDocumentoToSiglas = "ANU"
      Case eSD_REFINANCIADO: enumStatusDocumentoToSiglas = "RFI"
      Case Else: enumStatusDocumentoToSiglas = enumStatusDocumentoToSiglas(0)
   End Select
End Function
Public Sub FillComboBoxWithStatusDocumento(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusDocumentoToString(enum_StatusDocumento.eSD_PORCANCELAR)
      End If
      varComboBox.List(nCount) = enumStatusDocumentoToString(enum_StatusDocumento.eSD_PORCANCELAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDocumentoToString(enum_StatusDocumento.eSD_CANCELADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDocumentoToString(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDocumentoToString(enum_StatusDocumento.eSD_ABONADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDocumentoToString(enum_StatusDocumento.eSD_ANULADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDocumentoToString(enum_StatusDocumento.eSD_REFINANCIADO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeCxCToString(ByVal tipodeCuenta As enum_TipoDeTransaccion) As String
   Select Case tipodeCuenta
      Case eTD_FACTURA: enumTipoDeCxCToString = "Factura"
      Case eTD_GIRO: enumTipoDeCxCToString = "Giro"
      Case eTD_CHEQUEDEVUELTO: enumTipoDeCxCToString = "Cheque Devuelto"
      Case eTD_NOTADECREDITO: enumTipoDeCxCToString = "Nota de Crédito"
      Case eTD_NOTADEDEBITO: enumTipoDeCxCToString = "Nota de Débito"
      Case eTD_NOTAENTREGA: enumTipoDeCxCToString = "Nota de Entrega"
      Case eTD_NOASIGNADO: enumTipoDeCxCToString = "No Asignado"
      Case eTD_BOLETADEVENTA: enumTipoDeCxCToString = "Boleta de Venta"
      Case eTD_TICKETMAQUINAREGISTRADORA: enumTipoDeCxCToString = "Ticket Máquina Registradora"
      Case eTD_RECIBOPORHONORARIOS: enumTipoDeCxCToString = "Recibo por Honorarios"
      Case eTD_LIQUIDACIONDECOMPRA: enumTipoDeCxCToString = "Liquidación de Compra"
      Case eTD_OTROS: enumTipoDeCxCToString = "Otros"
      Case eTD_NOTADECREDITOCOMPROBANTEFISCAL: enumTipoDeCxCToString = "Nota de Crédito Comprobante Fiscal"
      Case Else: enumTipoDeCxCToString = enumTipoDeCxCToString(0)
      End Select
End Function

Public Function strTipoDeCxCToNum(ByVal tipodeCuenta As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeTransaccion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_FACTURA To eTD_NOTADECREDITOCOMPROBANTEFISCAL
   If UCase(tipodeCuenta) = UCase(enumTipoDeCxCToString(mEnum)) Then
      strTipoDeCxCToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeCxCToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipodeCuenta & " no existe en tipo enumerativo tipoDeCuenta"
   End If
h_EXIT:
   Exit Function
End Function
Public Function strTipoDeCxCToSiglasToString(ByVal tipodeCuenta As String) As String
   Dim mEnum As enum_TipoDeTransaccion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_FACTURA To eTD_NOTADECREDITOCOMPROBANTEFISCAL
      If UCase(tipodeCuenta) = UCase(enumTipoDeCxCToSiglas(mEnum)) Then
         strTipoDeCxCToSiglasToString = enumTipoDeCxCToString(mEnum)
         GoTo h_EXIT
      End If
   Next
   strTipoDeCxCToSiglasToString = enumTipoDeCxCToString(eTD_FACTURA)
h_EXIT:
   Exit Function
End Function
Public Function strTipoDeCxCToNumToSiglas(ByVal tipodeCuenta As String) As Integer
   Dim mEnum As enum_TipoDeTransaccion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_FACTURA To eTD_NOTADECREDITOCOMPROBANTEFISCAL
   If UCase(tipodeCuenta) = UCase(enumTipoDeCxCToSiglas(mEnum)) Then
      strTipoDeCxCToNumToSiglas = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeCxCToNumToSiglas = 0
h_EXIT:
   Exit Function
End Function
Public Function strTipoDeCxCToNumBoolean(ByVal tipodeCuenta As String) As Boolean
   Dim esUnTipoDeCxC As Boolean
   Dim mEnum As enum_TipoDeTransaccion
   Dim nCount As Integer
   esUnTipoDeCxC = False
   For mEnum = eTD_FACTURA To eTD_NOTADECREDITOCOMPROBANTEFISCAL
   If UCase(tipodeCuenta) = UCase(enumTipoDeCxCToString(mEnum)) Then
      esUnTipoDeCxC = True
      GoTo h_EXIT
   End If
   Next
h_EXIT:
   strTipoDeCxCToNumBoolean = esUnTipoDeCxC
   Exit Function
End Function
Public Function enumTipoDeCxCToSiglas(tipoDocumentoCxC As enum_TipoDeTransaccion) As String
Select Case tipoDocumentoCxC
      Case eTD_FACTURA: enumTipoDeCxCToSiglas = "FAC"
      Case eTD_GIRO: enumTipoDeCxCToSiglas = "GRO"
      Case eTD_CHEQUEDEVUELTO: enumTipoDeCxCToSiglas = "C/D"
      Case eTD_NOTADECREDITO: enumTipoDeCxCToSiglas = "N/C"
      Case eTD_NOTADEDEBITO: enumTipoDeCxCToSiglas = "N/D"
      Case eTD_NOTAENTREGA: enumTipoDeCxCToSiglas = "N/E"
      Case eTD_NOASIGNADO: enumTipoDeCxCToSiglas = "N/A"
      Case eTD_BOLETADEVENTA: enumTipoDeCxCToSiglas = "BOL"
      Case eTD_TICKETMAQUINAREGISTRADORA: enumTipoDeCxCToSiglas = "TIC"
      Case eTD_RECIBOPORHONORARIOS: enumTipoDeCxCToSiglas = "R/H"
      Case eTD_LIQUIDACIONDECOMPRA: enumTipoDeCxCToSiglas = "L/C"
      Case eTD_OTROS: enumTipoDeCxCToSiglas = "OTR"
      Case eTD_NOTADECREDITOCOMPROBANTEFISCAL: enumTipoDeCxCToSiglas = "N/C-CF"
      Case Else: enumTipoDeCxCToSiglas = enumTipoDeCxCToSiglas(0)
   End Select
End Function

Public Sub FillComboBoxWithTipodeCxC(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_FACTURA)
      End If
      varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_FACTURA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_GIRO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_CHEQUEDEVUELTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTADEDEBITO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTAENTREGA)
      If action <> insertar Then
         nCount = nCount + 1
         varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL)
         nCount = nCount + 1
         varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
      End If
      If gGlobalization.fEsCodigoPeru Then
         nCount = nCount + 1
         varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
         nCount = nCount + 1
         varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_RECIBOPORHONORARIOS)
         nCount = nCount + 1
         varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_LIQUIDACIONDECOMPRA)
         nCount = nCount + 1
         varComboBox.List(nCount) = enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_OTROS)
      End If
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub
Public Function enumOrigenFacturacionOManualToString(ByVal origenFacturacionOManual As enum_OrigenFacturacionOManual) As String
   Select Case origenFacturacionOManual
      Case eOF_FACTURA: enumOrigenFacturacionOManualToString = "Factura"
      Case eOF_MANUAL: enumOrigenFacturacionOManualToString = "Manual"
      Case eOF_S10: enumOrigenFacturacionOManualToString = "Sistema 10"
      Case Else: enumOrigenFacturacionOManualToString = enumOrigenFacturacionOManualToString(0)
   End Select
End Function
Public Function enumOrigenFacturacionOManualToSiglas(ByVal origenFacturacionOManual As enum_OrigenFacturacionOManual) As String
   Select Case origenFacturacionOManual
      Case eOF_FACTURA: enumOrigenFacturacionOManualToSiglas = "FAC"
      Case eOF_MANUAL: enumOrigenFacturacionOManualToSiglas = "MAN"
      Case eOF_S10: enumOrigenFacturacionOManualToSiglas = "S10"
      Case Else: enumOrigenFacturacionOManualToSiglas = enumOrigenFacturacionOManualToSiglas(0)
   End Select
End Function
Public Function strOrigenFacturacionOManualToNum(ByVal origenFacturacionOManual As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_OrigenFacturacionOManual
   Dim nCount As Integer
   nCount = 0
   For mEnum = eOF_FACTURA To eOF_S10
   If UCase(origenFacturacionOManual) = UCase(enumOrigenFacturacionOManualToString(mEnum)) Then
      strOrigenFacturacionOManualToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strOrigenFacturacionOManualToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & origenFacturacionOManual & " no existe en tipo enumerativo origenFacturacionOManual"
   End If
h_EXIT:
   Exit Function
End Function
Public Function strOrigenFacturacionOManualToNumToSiglas(ByVal origenFacturacionOManual As String) As Integer
   Dim mEnum As enum_OrigenFacturacionOManual
   Dim nCount As Integer
   nCount = 0
   For mEnum = eOF_FACTURA To eOF_S10
      If UCase(origenFacturacionOManual) = UCase(enumOrigenFacturacionOManualToSiglas(mEnum)) Then
         strOrigenFacturacionOManualToNumToSiglas = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strOrigenFacturacionOManualToNumToSiglas = 0
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithOrigenFacturacionOManual(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumOrigenFacturacionOManualToString(enum_OrigenFacturacionOManual.eOF_FACTURA)
      End If
      varComboBox.List(nCount) = enumOrigenFacturacionOManualToString(enum_OrigenFacturacionOManual.eOF_FACTURA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenFacturacionOManualToString(enum_OrigenFacturacionOManual.eOF_MANUAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenFacturacionOManualToString(enum_OrigenFacturacionOManual.eOF_S10)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumFormaDeEscogerCompaniaToString(ByVal formaDeEscogerCompania As enum_FormaDeEscogerCompania) As String
   Select Case formaDeEscogerCompania
      Case eFD_PORNOMBRE: enumFormaDeEscogerCompaniaToString = "Por nombre"
      Case eFD_PORCODIGO: enumFormaDeEscogerCompaniaToString = "Por código"
      Case Else: enumFormaDeEscogerCompaniaToString = enumFormaDeEscogerCompaniaToString(0)
   End Select
End Function

Public Function strFormaDeEscogerCompaniaToNum(ByVal formaDeEscogerCompania As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeEscogerCompania
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_PORNOMBRE To eFD_PORCODIGO
   If UCase(formaDeEscogerCompania) = UCase(enumFormaDeEscogerCompaniaToString(mEnum)) Then
      strFormaDeEscogerCompaniaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strFormaDeEscogerCompaniaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeEscogerCompania & " no existe en tipo enumerativo formaDeEscogerCompania"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeEscogerCompania(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORNOMBRE)
      End If
      varComboBox.List(nCount) = enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORNOMBRE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeEscogerCompaniaToString(enum_FormaDeEscogerCompania.eFD_PORCODIGO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusCobranzaToString(ByVal statusCobranza As enum_StatusCobranza) As String
   Select Case statusCobranza
      Case eSC_VIGENTE: enumStatusCobranzaToString = "Vigente"
      Case eSC_ANULADA: enumStatusCobranzaToString = "Anulada"
      Case Else: enumStatusCobranzaToString = enumStatusCobranzaToString(0)
   End Select
End Function

Public Function strStatusCobranzaToNum(ByVal statusCobranza As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCobranza
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_VIGENTE To eSC_ANULADA
      If UCase(statusCobranza) = UCase(enumStatusCobranzaToString(mEnum)) Then
         strStatusCobranzaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusCobranzaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusCobranza & " no existe en tipo enumerativo statusCobranza"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCobranza(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusCobranzaToString(enum_StatusCobranza.eSC_VIGENTE)
      End If
      varComboBox.List(nCount) = enumStatusCobranzaToString(enum_StatusCobranza.eSC_VIGENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusCobranzaToString(enum_StatusCobranza.eSC_ANULADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeAlicuotaToString(ByVal TipoDeAlicuota As enum_TipoDeAlicuota) As String
   Select Case TipoDeAlicuota
      Case eTD_EXENTO: enumTipoDeAlicuotaToString = "Exento"
      Case eTD_ALICUOTAGENERAL: enumTipoDeAlicuotaToString = "Alícuota General"
      Case eTD_ALICUOTA2: enumTipoDeAlicuotaToString = "Alícuota Reducida"
      Case eTD_ALICUOTA3: enumTipoDeAlicuotaToString = "Alícuota Adicional"
      Case eTD_EXENTONC: enumTipoDeAlicuotaToString = "Exento NC"
      Case eTD_ALICUOTAGENERALNC: enumTipoDeAlicuotaToString = "Alícuota General NC"
      Case eTD_ALICUOTA2NC: enumTipoDeAlicuotaToString = "Alícuota Reducida NC"
      Case eTD_ALICUOTA3NC: enumTipoDeAlicuotaToString = "Alícuota Adicional NC"
      Case Else: enumTipoDeAlicuotaToString = enumTipoDeAlicuotaToString(0)
   End Select
End Function

Public Function strTipoDeAlicuotaToNum(ByVal TipoDeAlicuota As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeAlicuota
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_EXENTO To eTD_ALICUOTA3NC
   If UCase(TipoDeAlicuota) = UCase(enumTipoDeAlicuotaToString(mEnum)) Then
      strTipoDeAlicuotaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeAlicuotaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeAlicuota & " no existe en tipo enumerativo tipoDeAlicuota"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeAlicuota(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeAlicuotaToString(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL)
      End If
      varComboBox.List(nCount) = enumTipoDeAlicuotaToString(enum_TipoDeAlicuota.eTD_EXENTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeAlicuotaToString(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeAlicuotaToString(enum_TipoDeAlicuota.eTD_ALICUOTA2)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeAlicuotaToString(enum_TipoDeAlicuota.eTD_ALICUOTA3)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = 1  ' Se selecciona el valor por defecto
      End If
   End If
End Sub

Public Function enumStatusArticuloToString(ByVal statusArticulo As enum_StatusArticulo) As String
   Select Case statusArticulo
      Case eSA_VIGENTE: enumStatusArticuloToString = "Vigente"
      Case eSA_DESINCORPORADO: enumStatusArticuloToString = "Desincorporado"
      Case Else: enumStatusArticuloToString = enumStatusArticuloToString(0)
   End Select
End Function

Public Function strStatusArticuloToNum(ByVal statusArticulo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusArticulo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSA_VIGENTE To eSA_DESINCORPORADO
   If UCase(statusArticulo) = UCase(enumStatusArticuloToString(mEnum)) Then
      strStatusArticuloToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusArticuloToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusArticulo & " no existe en tipo enumerativo statusArticulo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusArticulo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusArticuloToString(enum_StatusArticulo.eSA_VIGENTE)
      End If
      varComboBox.List(nCount) = enumStatusArticuloToString(enum_StatusArticulo.eSA_VIGENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusArticuloToString(enum_StatusArticulo.eSA_DESINCORPORADO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeProductoToString(ByVal tipoDeProducto As enum_TipoDeProducto) As String
   Select Case tipoDeProducto
      Case eTD_TP_NUEVO: enumTipoDeProductoToString = "Nuevo"
      Case eTD_TP_ACTUALIZACION: enumTipoDeProductoToString = "Actualización"
      Case eTD_TP_OTRO: enumTipoDeProductoToString = "Otro"
      Case eTD_TP_POLIZA: enumTipoDeProductoToString = "Póliza"
      Case Else: enumTipoDeProductoToString = enumTipoDeProductoToString(0)
   End Select
End Function

Public Function strTipoDeProductoToNum(ByVal tipoDeProducto As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeProducto
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_TP_NUEVO To eTD_TP_POLIZA
   If UCase(tipoDeProducto) = UCase(enumTipoDeProductoToString(mEnum)) Then
      strTipoDeProductoToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeProductoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeProducto & " no existe en tipo enumerativo tipoDeProducto"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeProducto(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeProductoToString(enum_TipoDeProducto.eTD_TP_NUEVO)
      End If
      varComboBox.List(nCount) = enumTipoDeProductoToString(enum_TipoDeProducto.eTD_TP_NUEVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeProductoToString(enum_TipoDeProducto.eTD_TP_ACTUALIZACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeProductoToString(enum_TipoDeProducto.eTD_TP_OTRO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeProductoToString(enum_TipoDeProducto.eTD_TP_POLIZA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusFacturaToString(ByVal statusFactura As enum_StatusFactura) As String
   Select Case statusFactura
      Case eSF_EMITIDA: enumStatusFacturaToString = "Emitida"
      Case eSF_ANULADA: enumStatusFacturaToString = "Anulada"
      Case eSF_BORRADOR: enumStatusFacturaToString = "Borrador"
      Case Else: enumStatusFacturaToString = enumStatusFacturaToString(0)
   End Select
End Function

Public Function strStatusFacturaToNum(ByVal statusFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSF_EMITIDA To eSF_BORRADOR
   If UCase(statusFactura) = UCase(enumStatusFacturaToString(mEnum)) Then
      strStatusFacturaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusFactura & " no existe en tipo enumerativo statusFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusFacturaToString(enum_StatusFactura.eSF_EMITIDA)
      End If
      varComboBox.List(nCount) = enumStatusFacturaToString(enum_StatusFactura.eSF_EMITIDA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusFacturaToString(enum_StatusFactura.eSF_ANULADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusFacturaToString(enum_StatusFactura.eSF_BORRADOR)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipodocumentoFacturaToString(ByVal tipodocumentoFactura As enum_TipoDocumentoFactura) As String
   Select Case tipodocumentoFactura
      Case eTF_FACTURA: enumTipodocumentoFacturaToString = "Factura"
      Case eTF_NOTADECREDITO: enumTipodocumentoFacturaToString = "Nota de Crédito"
      Case eTF_NOTADEDEBITO: enumTipodocumentoFacturaToString = "Nota de Débito"
      Case eTF_RESUMENDIARIODEVENTAS: enumTipodocumentoFacturaToString = "Resumen Diario de Ventas"
      Case eTF_NOASIGNADO: enumTipodocumentoFacturaToString = "No Asignado"
      Case eTF_COMPROBANTEFISCAL: enumTipodocumentoFacturaToString = "Comprobante Fiscal"
      Case eTF_BOLETA: enumTipodocumentoFacturaToString = "Boleta"
      Case eTF_NOTADECREDITOCOMPROBANTEFISCAL: enumTipodocumentoFacturaToString = "Nota de Crédito Comprobante Fiscal"
      Case eTF_NOTAENTREGA: enumTipodocumentoFacturaToString = "Nota de Entrega"
      Case Else: enumTipodocumentoFacturaToString = enumTipodocumentoFacturaToString(0)
      End Select
End Function

Public Function strTipodocumentoFacturaToNum(ByVal tipodocumentoFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDocumentoFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTF_FACTURA To eTF_NOTAENTREGA
   If UCase(tipodocumentoFactura) = UCase(enumTipodocumentoFacturaToString(mEnum)) Then
      strTipodocumentoFacturaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipodocumentoFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipodocumentoFactura & " no existe en tipo enumerativo tipodocumentoFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipodocumentoFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_FACTURA)
   End If
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_FACTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_BOLETA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumFormadePagoToString(ByVal formadePago As enum_FormaDePago) As String
   Select Case formadePago
      Case eFP_CONTADO: enumFormadePagoToString = "Contado"
      Case eFP_CREDITO: enumFormadePagoToString = "Crédito"
      Case Else: enumFormadePagoToString = enumFormadePagoToString(0)
   End Select
End Function

Public Function strFormadePagoToNum(ByVal formadePago As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDePago
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFP_CONTADO To eFP_CREDITO
   If UCase(formadePago) = UCase(enumFormadePagoToString(mEnum)) Then
      strFormadePagoToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strFormadePagoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formadePago & " no existe en tipo enumerativo formadePago"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormadePago(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumFormadePagoToString(enum_FormaDePago.eFP_CONTADO)
      End If
      varComboBox.List(nCount) = enumFormadePagoToString(enum_FormaDePago.eFP_CONTADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormadePagoToString(enum_FormaDePago.eFP_CREDITO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumContadoCreditoToString(ByVal contadoCredito As enum_ContadoCredito) As String
   Select Case contadoCredito
      Case eCC_CONTADO: enumContadoCreditoToString = "Contado"
      Case eCC_Credito: enumContadoCreditoToString = "Crédito"
      Case eCC_CLAVE_TEMPORAL: enumContadoCreditoToString = "Clave Temporal"
      Case Else: enumContadoCreditoToString = enumContadoCreditoToString(0)
   End Select
End Function

Public Function enumDigitoContadoCreditoToChar(contadoCredito As enum_ContadoCredito) As String
   Select Case contadoCredito
      Case eCC_CONTADO: enumDigitoContadoCreditoToChar = "0"
      Case eCC_Credito: enumDigitoContadoCreditoToChar = "1"
      Case eCC_CLAVE_TEMPORAL: enumDigitoContadoCreditoToChar = "2"
      Case Else: enumDigitoContadoCreditoToChar = "0"
   End Select
End Function

Public Function strContadoCreditoToNum(ByVal contadoCredito As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ContadoCredito
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCC_CONTADO To eCC_CLAVE_TEMPORAL
   If UCase(contadoCredito) = UCase(enumContadoCreditoToString(mEnum)) Then
      strContadoCreditoToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strContadoCreditoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & contadoCredito & " no existe en tipo enumerativo contadoCredito"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithContadoCredito(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumContadoCreditoToString(enum_ContadoCredito.eCC_CONTADO)
      End If
      varComboBox.List(nCount) = enumContadoCreditoToString(enum_ContadoCredito.eCC_CONTADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumContadoCreditoToString(enum_ContadoCredito.eCC_Credito)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumContadoCreditoToString(enum_ContadoCredito.eCC_CLAVE_TEMPORAL)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumMonoMultiUsuarioToString(ByVal monoMultiusuario As enum_MonoMultiUsuario) As String
   Select Case monoMultiusuario
      Case eMM_MONOUSUARIO: enumMonoMultiUsuarioToString = "Monousuario"
      Case eMM_MULTIUSUARIO: enumMonoMultiUsuarioToString = "Multiusuario"
      Case eMM_SQL_SERVER: enumMonoMultiUsuarioToString = "SQL Server - BD"
      Case Else: enumMonoMultiUsuarioToString = enumMonoMultiUsuarioToString(0)
   End Select
End Function

Public Function strMonoMultiUsuarioToNum(ByVal monoMultiusuario As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MonoMultiUsuario
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMM_MONOUSUARIO To eMM_SQL_SERVER
   If UCase(monoMultiusuario) = UCase(enumMonoMultiUsuarioToString(mEnum)) Then
      strMonoMultiUsuarioToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strMonoMultiUsuarioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & monoMultiusuario & " no existe en tipo enumerativo monoMultiUsuario"
   End If
h_EXIT:
   Exit Function
End Function

Public Function StatusFacturaStringToEnum(ByVal valStatusFactura As String) As enum_StatusFactura
   If UCase(valStatusFactura) = UCase("Emitida") Then
      StatusFacturaStringToEnum = eSF_EMITIDA
   ElseIf UCase(valStatusFactura) = UCase("Anulada") Then
      StatusFacturaStringToEnum = eSF_ANULADA
   ElseIf UCase(valStatusFactura) = UCase("Borrador") Then
      StatusFacturaStringToEnum = eSF_BORRADOR
   Else
      StatusFacturaStringToEnum = eSF_BORRADOR
   End If
End Function

Public Sub FillComboBoxWithMonoMultiUsuario(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumMonoMultiUsuarioToString(enum_MonoMultiUsuario.eMM_MONOUSUARIO)
      End If
      varComboBox.List(nCount) = enumMonoMultiUsuarioToString(enum_MonoMultiUsuario.eMM_MONOUSUARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMonoMultiUsuarioToString(enum_MonoMultiUsuario.eMM_MULTIUSUARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMonoMultiUsuarioToString(enum_MonoMultiUsuario.eMM_SQL_SERVER)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeSAWToString(tipoDeSAW As enum_TipoDeSAW) As String
   Select Case tipoDeSAW
      Case eTD_SAW_BASICO: enumTipoDeSAWToString = "Básico"
      Case eTD_SAW_PROFESIONAL: enumTipoDeSAWToString = "Profesional"
      Case eTD_SAW_CONTRIBUYENTES_ESPECIALES: enumTipoDeSAWToString = "Contribuyentes Especiales"
      Case Else
         enumTipoDeSAWToString = enumTipoDeSAWToString(0)
         End Select
End Function

Public Function strTipoDeSAWToNum(ByVal tipoDeSAW As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeSAW
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_SAW_BASICO To eTD_SAW_CONTRIBUYENTES_ESPECIALES
      If UCase(tipoDeSAW) = UCase(enumTipoDeSAWToString(mEnum)) Then
         strTipoDeSAWToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeSAWToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeSAW & " no existe en tipo enumerativo tipoDeSAW"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeSAW(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeSAWToString(enum_TipoDeSAW.eTD_SAW_BASICO)
      End If
      varComboBox.List(nCount) = enumTipoDeSAWToString(enum_TipoDeSAW.eTD_SAW_BASICO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeSAWToString(enum_TipoDeSAW.eTD_SAW_PROFESIONAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeSAWToString(enum_TipoDeSAW.eTD_SAW_CONTRIBUYENTES_ESPECIALES)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub


Public Function enumTipoDeAutorizacionToString(ByVal tipoDeAutorizacion As enum_TipoDeAutorizacion) As String
   Select Case tipoDeAutorizacion
      Case eTD_NUEVAINSTALACION: enumTipoDeAutorizacionToString = "NuevaInstalacion"
      Case eTD_REINSTALACION: enumTipoDeAutorizacionToString = "Reinstalacion"
      Case Else: enumTipoDeAutorizacionToString = enumTipoDeAutorizacionToString(0)
   End Select
End Function

Public Function strTipoDeAutorizacionToNum(ByVal tipoDeAutorizacion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeAutorizacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_NUEVAINSTALACION To eTD_REINSTALACION
   If UCase(tipoDeAutorizacion) = UCase(enumTipoDeAutorizacionToString(mEnum)) Then
      strTipoDeAutorizacionToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeAutorizacionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeAutorizacion & " no existe en tipo enumerativo tipoDeAutorizacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeAutorizacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeAutorizacionToString(enum_TipoDeAutorizacion.eTD_NUEVAINSTALACION)
   End If
   varComboBox.List(nCount) = enumTipoDeAutorizacionToString(enum_TipoDeAutorizacion.eTD_NUEVAINSTALACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeAutorizacionToString(enum_TipoDeAutorizacion.eTD_REINSTALACION)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumTipoDeFacturaToString(ByVal tipoDeFactura As enum_TipoDeFactura) As String
   Select Case tipoDeFactura
      Case eTD_OFICINA: enumTipoDeFacturaToString = "Oficina"
      Case eTD_DISTRIBUIDOR: enumTipoDeFacturaToString = "Distribuidor"
      Case eTD_PROMOCION: enumTipoDeFacturaToString = "Promoción"
      Case Else: enumTipoDeFacturaToString = enumTipoDeFacturaToString(0)
   End Select
End Function

Public Function strTipoDeFacturaToNum(ByVal tipoDeFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_OFICINA To eTD_PROMOCION
   If UCase(tipoDeFactura) = UCase(enumTipoDeFacturaToString(mEnum)) Then
      strTipoDeFacturaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strTipoDeFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeFactura & " no existe en tipo enumerativo tipoDeFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeFacturaToString(enum_TipoDeFactura.eTD_OFICINA)
      End If
      varComboBox.List(nCount) = enumTipoDeFacturaToString(enum_TipoDeFactura.eTD_OFICINA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeFacturaToString(enum_TipoDeFactura.eTD_DISTRIBUIDOR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeFacturaToString(enum_TipoDeFactura.eTD_PROMOCION)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumOrigenLlamadaToString(ByVal origenLlamada As enum_OrigenLlamada) As String
   Select Case origenLlamada
      Case eOL_CLIENTE: enumOrigenLlamadaToString = "Cliente"
      Case eOL_PRENSA: enumOrigenLlamadaToString = "Prensa"
      Case eOL_MAILING: enumOrigenLlamadaToString = "Mailing"
      Case eOL_REFERIDO: enumOrigenLlamadaToString = "Referido"
      Case eOL_RADIO: enumOrigenLlamadaToString = "Radio"
      Case eOL_PAGINASAMARRILLAS: enumOrigenLlamadaToString = "Páginas Amarrillas"
      Case eOL_EVENTO: enumOrigenLlamadaToString = "Evento"
      Case eOL_COMUNICADOFAX: enumOrigenLlamadaToString = "Comunicado Fax"
      Case eOL_OFICINA: enumOrigenLlamadaToString = "Oficina"
      Case eOL_WWWGALACCOM: enumOrigenLlamadaToString = "wwwGalacCom"
      Case Else: enumOrigenLlamadaToString = enumOrigenLlamadaToString(0)
   End Select
End Function

Public Function strOrigenLlamadaToNum(ByVal origenLlamada As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_OrigenLlamada
   Dim nCount As Integer
   nCount = 0
   For mEnum = eOL_CLIENTE To eOL_WWWGALACCOM
   If UCase(origenLlamada) = UCase(enumOrigenLlamadaToString(mEnum)) Then
      strOrigenLlamadaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strOrigenLlamadaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & origenLlamada & " no existe en tipo enumerativo origenLlamada"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithOrigenLlamada(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_CLIENTE)
      End If
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_CLIENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_PRENSA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_MAILING)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_REFERIDO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_RADIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_PAGINASAMARRILLAS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_EVENTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_COMUNICADOFAX)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_OFICINA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOrigenLlamadaToString(enum_OrigenLlamada.eOL_WWWGALACCOM)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumAccionToString(ByVal accion As enum_Accion) As String
   Select Case accion
      Case eAc_NINGUNA: enumAccionToString = "Ninguna"
      Case eAc_COTIZAR: enumAccionToString = "Cotizar"
      Case eAc_FACTURAR: enumAccionToString = "Facturar"
      Case eAc_PROBLEMA: enumAccionToString = "Problema"
      Case eAc_REPOSICION: enumAccionToString = "Reposición"
      Case eAc_RELLAMAR: enumAccionToString = "Rellamar"
      Case Else: enumAccionToString = enumAccionToString(0)
   End Select
End Function

Public Function strAccionToNum(ByVal accion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Accion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAc_NINGUNA To eAc_RELLAMAR
   If UCase(accion) = UCase(enumAccionToString(mEnum)) Then
      strAccionToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strAccionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & accion & " no existe en tipo enumerativo accion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithAccion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumAccionToString(enum_Accion.eAc_NINGUNA)
      End If
      varComboBox.List(nCount) = enumAccionToString(enum_Accion.eAc_NINGUNA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAccionToString(enum_Accion.eAc_COTIZAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAccionToString(enum_Accion.eAc_FACTURAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAccionToString(enum_Accion.eAc_PROBLEMA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAccionToString(enum_Accion.eAc_REPOSICION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAccionToString(enum_Accion.eAc_RELLAMAR)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoLLamadaToString(ByVal tipoLLamada As enum_TipoLLamada) As String
   Select Case tipoLLamada
      Case eTL_VENTA: enumTipoLLamadaToString = "VENTA"
      Case eTL_SERVICIO: enumTipoLLamadaToString = "SERVICIO"
      Case eTL_OFICINA: enumTipoLLamadaToString = "OFICINA"
      Case eTL_PROMOCION: enumTipoLLamadaToString = "Promoción"
      Case eTL_CLAVE: enumTipoLLamadaToString = "Clave"
      Case eTL_COBRANZA: enumTipoLLamadaToString = "Cobranza"
      Case eTL_INTERNET: enumTipoLLamadaToString = "Internet"
      Case eTL_POST_VENTA: enumTipoLLamadaToString = "Post-Venta"
      Case eTL_NUEVOS: enumTipoLLamadaToString = "Nuevos"
      Case eTL_OFFICE_SUPPORT: enumTipoLLamadaToString = "Office Support"
      Case eTL_POR_DEFINIR: enumTipoLLamadaToString = "Por Definir"
      Case eTL_INSTALACION: enumTipoLLamadaToString = "Instalación"
      Case eTL_CONFIGURACION: enumTipoLLamadaToString = "Configuración"
      Case eTL_IMPRESION: enumTipoLLamadaToString = "Impresión"
      Case Else: enumTipoLLamadaToString = enumTipoLLamadaToString(0)
   End Select
End Function

Public Function strTipoLLamadaToNum(ByVal tipoLLamada As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoLLamada
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTL_VENTA To eTL_IMPRESION
      If UCase(tipoLLamada) = UCase(enumTipoLLamadaToString(mEnum)) Then
         strTipoLLamadaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoLLamadaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoLLamada & " no existe en tipo enumerativo tipoLLamada"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoLLamada(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoLLamadaToString(enum_TipoLLamada.eTL_VENTA)
      End If
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_VENTA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_SERVICIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_OFICINA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_PROMOCION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_CLAVE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_COBRANZA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_INTERNET)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_POST_VENTA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_NUEVOS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_OFFICE_SUPPORT)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_POR_DEFINIR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_INSTALACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_CONFIGURACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoLLamadaToString(enum_TipoLLamada.eTL_IMPRESION)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = enum_TipoLLamada.eTL_POR_DEFINIR  ' Se selecciona el valor por defecto
      End If
   End If
End Sub

Public Function enumMananaOTardeToString(ByVal mananaOTarde As enum_MananaOTarde) As String
   Select Case mananaOTarde
      Case eMO_MANANA: enumMananaOTardeToString = "Mañana"
      Case eMO_TARDE: enumMananaOTardeToString = "Tarde"
      Case eMO_TODOELDIA: enumMananaOTardeToString = "Todo el día"
      Case Else: enumMananaOTardeToString = enumMananaOTardeToString(0)
   End Select
End Function

Public Function strMananaOTardeToNum(ByVal mananaOTarde As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MananaOTarde
   Dim nCount As Integer
   nCount = 0
   'For mEnum = eMO_MANANA To eMO_TARDE
   For mEnum = eMO_MANANA To eMO_TODOELDIA
      If UCase(mananaOTarde) = UCase(enumMananaOTardeToString(mEnum)) Then
         strMananaOTardeToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMananaOTardeToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & mananaOTarde & " no existe en tipo enumerativo mananaOTarde"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMananaOTarde(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumMananaOTardeToString(enum_MananaOTarde.eMO_MANANA)
      End If
      varComboBox.List(nCount) = enumMananaOTardeToString(enum_MananaOTarde.eMO_MANANA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMananaOTardeToString(enum_MananaOTarde.eMO_TARDE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMananaOTardeToString(enum_MananaOTarde.eMO_TODOELDIA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusVisitaToString(ByVal statusVisita As enum_StatusVisita) As String
   Select Case statusVisita
      Case eSV_VIGENTE: enumStatusVisitaToString = "Vigente"
      Case eSV_CANCELADA: enumStatusVisitaToString = "Cancelada"
      Case eSV_REASIGNADA: enumStatusVisitaToString = "Reasignada"
      Case eSV_REALIZADA: enumStatusVisitaToString = "Realizada"
      Case Else: enumStatusVisitaToString = enumStatusVisitaToString(0)
   End Select
End Function

Public Function strStatusVisitaToNum(ByVal statusVisita As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusVisita
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSV_VIGENTE To eSV_REALIZADA
      If UCase(statusVisita) = UCase(enumStatusVisitaToString(mEnum)) Then
         strStatusVisitaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusVisitaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusVisita & " no existe en tipo enumerativo statusVisita"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusVisita(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusVisitaToString(enum_StatusVisita.eSV_VIGENTE)
      End If
      varComboBox.List(nCount) = enumStatusVisitaToString(enum_StatusVisita.eSV_VIGENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusVisitaToString(enum_StatusVisita.eSV_CANCELADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusVisitaToString(enum_StatusVisita.eSV_REASIGNADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusVisitaToString(enum_StatusVisita.eSV_REALIZADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTarjetaToString(ByVal tarjeta As enum_Tarjeta) As String
   Select Case tarjeta
      Case eTa_VISA: enumTarjetaToString = "Visa"
      Case eTa_MASTER: enumTarjetaToString = "Master"
      Case eTa_AMERICAN: enumTarjetaToString = "American"
      Case eTa_DEBITO: enumTarjetaToString = "Debito"
      Case Else: enumTarjetaToString = enumTarjetaToString(0)
   End Select
End Function

Public Function strTarjetaToNum(ByVal tarjeta As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Tarjeta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTa_VISA To eTa_DEBITO
      If UCase(tarjeta) = UCase(enumTarjetaToString(mEnum)) Then
         strTarjetaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTarjetaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tarjeta & " no existe en tipo enumerativo tarjeta"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTarjeta(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTarjetaToString(enum_Tarjeta.eTa_VISA)
      End If
      varComboBox.List(nCount) = enumTarjetaToString(enum_Tarjeta.eTa_VISA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTarjetaToString(enum_Tarjeta.eTa_MASTER)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTarjetaToString(enum_Tarjeta.eTa_AMERICAN)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTarjetaToString(enum_Tarjeta.eTa_DEBITO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeAlmacenToString(ByVal tipoDeAlmacen As enum_TipoDeAlmacen) As String
   Select Case tipoDeAlmacen
      Case eTD_PRINCIPAL: enumTipoDeAlmacenToString = "Principal"
      Case eTD_SECUNDARIO: enumTipoDeAlmacenToString = "Secundario"
      Case eTD_CONSIGNACION: enumTipoDeAlmacenToString = "Consignación"
      Case eTD_MERCANCIA_EN_TIENDA: enumTipoDeAlmacenToString = "Mercancía en tienda"
      Case Else: enumTipoDeAlmacenToString = enumTipoDeAlmacenToString(0)
   End Select
End Function

Public Function strTipoDeAlmacenToNum(ByVal tipoDeAlmacen As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeAlmacen
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_PRINCIPAL To eTD_MERCANCIA_EN_TIENDA
      If UCase(tipoDeAlmacen) = UCase(enumTipoDeAlmacenToString(mEnum)) Then
         strTipoDeAlmacenToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeAlmacenToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeAlmacen & " no existe en tipo enumerativo tipoDeAlmacen"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeAlmacen(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeAlmacenToString(enum_TipoDeAlmacen.eTD_PRINCIPAL)
      End If
      varComboBox.List(nCount) = enumTipoDeAlmacenToString(enum_TipoDeAlmacen.eTD_PRINCIPAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeAlmacenToString(enum_TipoDeAlmacen.eTD_SECUNDARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeAlmacenToString(enum_TipoDeAlmacen.eTD_CONSIGNACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeAlmacenToString(enum_TipoDeAlmacen.eTD_MERCANCIA_EN_TIENDA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeOperacionToString(ByVal TipoDeOperacion As enum_TipodeOperacion) As String
   Select Case TipoDeOperacion
      Case eTO_ENTRADA_DE_INVENTARIO: enumTipoDeOperacionToString = "Entrada de Inventario"
      Case eTO_SALIDA_DE_INVENTARIO: enumTipoDeOperacionToString = "Salida de Inventario"
      Case eTO_Autoconsumo: enumTipoDeOperacionToString = "Autoconsumo"
      Case eTO_Retiro: enumTipoDeOperacionToString = "Retiro"
      Case Else: enumTipoDeOperacionToString = enumTipoDeOperacionToString(0)
   End Select
End Function

Public Function strTipoDeOperacionToNum(ByVal TipoDeOperacion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipodeOperacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTO_ENTRADA_DE_INVENTARIO To enum_TipodeOperacion.eTO_Retiro
      If UCase(TipoDeOperacion) = UCase(enumTipoDeOperacionToString(mEnum)) Then
         strTipoDeOperacionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeOperacionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeOperacion & " no existe en tipo enumerativo tipodeOperacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeOperacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeOperacionToString(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO)
      End If
      varComboBox.List(nCount) = enumTipoDeOperacionToString(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeOperacionToString(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionToString(enum_TipodeOperacion.eTO_Autoconsumo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionToString(enum_TipodeOperacion.eTO_Retiro)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeArticuloToString(ByVal tipoDeArticulo As enum_TipoDeArticulo) As String
   Select Case tipoDeArticulo
      Case eTD_MERCANCIA: enumTipoDeArticuloToString = "Mercancía"
      Case eTD_SERVICIO: enumTipoDeArticuloToString = "Servicio"
      Case eTD_PRODUCTO_COMPUESTO: enumTipoDeArticuloToString = "Producto Compuesto"
      Case Else: enumTipoDeArticuloToString = enumTipoDeArticuloToString(0)
   End Select
End Function

Public Function strTipoDeArticuloToNum(ByVal tipoDeArticulo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeArticulo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_MERCANCIA To eTD_PRODUCTO_COMPUESTO
      If UCase(tipoDeArticulo) = UCase(enumTipoDeArticuloToString(mEnum)) Then
         strTipoDeArticuloToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeArticuloToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeArticulo & " no existe en tipo enumerativo tipoDeArticulo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeArticulo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeArticuloToString(enum_TipoDeArticulo.eTD_MERCANCIA)
      End If
      varComboBox.List(nCount) = enumTipoDeArticuloToString(enum_TipoDeArticulo.eTD_MERCANCIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeArticuloToString(enum_TipoDeArticulo.eTD_SERVICIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeArticuloToString(enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeServicioToString(ByVal tipoDeServicio As enum_TipoDeServicio) As String
   Select Case tipoDeServicio
      Case eTD_ACTUALIZACION: enumTipoDeServicioToString = "Actualización"
      Case eTD_ADIESTRAMIENTO: enumTipoDeServicioToString = "Adiestramiento"
      Case eTD_DEMOSTRACION: enumTipoDeServicioToString = "Demostración"
      Case eTD_GARANTIA: enumTipoDeServicioToString = "Garantía"
      Case eTD_INSTALACION: enumTipoDeServicioToString = "Instalación"
      Case eTD_REPOSICION: enumTipoDeServicioToString = "Reposición"
      Case eTD_OTRO: enumTipoDeServicioToString = "Otro"
      Case Else: enumTipoDeServicioToString = enumTipoDeServicioToString(0)
   End Select
End Function

Public Function strTipoDeServicioToNum(ByVal tipoDeServicio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeServicio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_ACTUALIZACION To eTD_OTRO
      If UCase(tipoDeServicio) = UCase(enumTipoDeServicioToString(mEnum)) Then
         strTipoDeServicioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeServicioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeServicio & " no existe en tipo enumerativo tipoDeServicio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeServicio(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_ACTUALIZACION)
      End If
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_ACTUALIZACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_ADIESTRAMIENTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_DEMOSTRACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_GARANTIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_INSTALACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_REPOSICION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeServicioToString(enum_TipoDeServicio.eTD_OTRO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumNivelDePrecioToString(ByVal nivelDePrecio As enum_NivelDePrecio) As String
   Select Case nivelDePrecio
      Case eND_PRECIO_1: enumNivelDePrecioToString = "Precio 1"
      Case eND_PRECIO_2: enumNivelDePrecioToString = "Precio 2"
      Case eND_PRECIO_3: enumNivelDePrecioToString = "Precio 3"
      Case eND_PRECIO_4: enumNivelDePrecioToString = "Precio 4"
      Case Else: enumNivelDePrecioToString = enumNivelDePrecioToString(0)
   End Select
End Function

Public Function strNivelDePrecioToNum(ByVal nivelDePrecio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_NivelDePrecio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eND_PRECIO_1 To eND_PRECIO_4
      If UCase(nivelDePrecio) = UCase(enumNivelDePrecioToString(mEnum)) Then
         strNivelDePrecioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strNivelDePrecioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & nivelDePrecio & " no existe en tipo enumerativo nivelDePrecio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithNivelDePrecio(ByRef varComboBox As Object, _
               UsaNivel1 As Boolean, UsaNivel2 As Boolean, _
               UsaNivel3 As Boolean, UsaNivel4 As Boolean, _
               Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumNivelDePrecioToString(enum_NivelDePrecio.eND_PRECIO_1)
      End If
      
      If UsaNivel1 Or (action <> insertar And action <> Modificar) Then
         varComboBox.List(nCount) = enumNivelDePrecioToString(enum_NivelDePrecio.eND_PRECIO_1)
         nCount = nCount + 1
      End If
      
      If UsaNivel2 Or (action <> insertar And action <> Modificar) Then
         varComboBox.List(nCount) = enumNivelDePrecioToString(enum_NivelDePrecio.eND_PRECIO_2)
         nCount = nCount + 1
      End If
      
      If UsaNivel3 Or (action <> insertar And action <> Modificar) Then
         varComboBox.List(nCount) = enumNivelDePrecioToString(enum_NivelDePrecio.eND_PRECIO_3)
         nCount = nCount + 1
      End If
      
      If UsaNivel4 Or (action <> insertar And action <> Modificar) Then
         varComboBox.List(nCount) = enumNivelDePrecioToString(enum_NivelDePrecio.eND_PRECIO_4)
      End If
      
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusDespachoToString(ByVal statusDespacho As enum_StatusDespacho) As String
   Select Case statusDespacho
      Case eED_PENDIENTE: enumStatusDespachoToString = "Pendiente"
      Case eED_ENVIADO: enumStatusDespachoToString = "Enviado"
      Case eED_ANULADO: enumStatusDespachoToString = "Anulado"
      Case Else: enumStatusDespachoToString = enumStatusDespachoToString(0)
   End Select
End Function

Public Function strStatusDespachoToNum(ByVal statusDespacho As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusDespacho
   Dim nCount As Integer
   nCount = 0
   For mEnum = eED_PENDIENTE To eED_ANULADO
      If UCase(statusDespacho) = UCase(enumStatusDespachoToString(mEnum)) Then
         strStatusDespachoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusDespachoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusDespacho & " no existe en tipo enumerativo statusDespacho"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusDespacho(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is Object  Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusDespachoToString(enum_StatusDespacho.eED_PENDIENTE)
      End If
      varComboBox.List(nCount) = enumStatusDespachoToString(enum_StatusDespacho.eED_PENDIENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDespachoToString(enum_StatusDespacho.eED_ENVIADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDespachoToString(enum_StatusDespacho.eED_ANULADO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumCourierToString(ByVal courier As enum_Courier) As String
   Select Case courier
      Case eCo_MENSAJEROS_RADIO_MRW: enumCourierToString = "Mensajeros Radio MRW"
      Case eCo_SUPER_ENVIOS: enumCourierToString = "Super Envios"
      Case eCo_MENSAJERO_GALAC: enumCourierToString = "Mensajero Galac"
      Case eCo_OFICINA: enumCourierToString = "Oficina"
      Case Else: enumCourierToString = enumCourierToString(0)
   End Select
End Function

Public Function strCourierToNum(ByVal courier As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Courier
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCo_MENSAJEROS_RADIO_MRW To eCo_OFICINA
      If UCase(courier) = UCase(enumCourierToString(mEnum)) Then
         strCourierToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCourierToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & courier & " no existe en tipo enumerativo courier"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCourier(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumCourierToString(enum_Courier.eCo_MENSAJEROS_RADIO_MRW)
      End If
      varComboBox.List(nCount) = enumCourierToString(enum_Courier.eCo_MENSAJEROS_RADIO_MRW)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCourierToString(enum_Courier.eCo_SUPER_ENVIOS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCourierToString(enum_Courier.eCo_MENSAJERO_GALAC)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCourierToString(enum_Courier.eCo_OFICINA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeDespachoToString(ByVal tipoDeDespacho As enum_TipoDeDespacho) As String
   Select Case tipoDeDespacho
      Case eTD_NORMAL: enumTipoDeDespachoToString = "Normal"
      Case eTD_FLETE_DESTINO: enumTipoDeDespachoToString = "Flete Destino"
      Case eTD_COD: enumTipoDeDespachoToString = "C.O.D."
      Case Else: enumTipoDeDespachoToString = enumTipoDeDespachoToString(0)
   End Select
End Function

Public Function strTipoDeDespachoToNum(ByVal tipoDeDespacho As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeDespacho
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_NORMAL To eTD_COD
      If UCase(tipoDeDespacho) = UCase(enumTipoDeDespachoToString(mEnum)) Then
         strTipoDeDespachoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeDespachoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeDespacho & " no existe en tipo enumerativo tipoDeDespacho"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeDespacho(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeDespachoToString(enum_TipoDeDespacho.eTD_NORMAL)
      End If
      varComboBox.List(nCount) = enumTipoDeDespachoToString(enum_TipoDeDespacho.eTD_NORMAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeDespachoToString(enum_TipoDeDespacho.eTD_FLETE_DESTINO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeDespachoToString(enum_TipoDeDespacho.eTD_COD)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumLugarDespachoToString(ByVal lugarDespacho As enum_LugarDespacho) As String
   Select Case lugarDespacho
      Case eLD_INTERIOR: enumLugarDespachoToString = "Interior"
      Case eLD_CARACAS: enumLugarDespachoToString = "Caracas"
      Case Else: enumLugarDespachoToString = enumLugarDespachoToString(0)
   End Select
End Function

Public Function strLugarDespachoToNum(ByVal lugarDespacho As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_LugarDespacho
   Dim nCount As Integer
   nCount = 0
   For mEnum = eLD_INTERIOR To eLD_CARACAS
      If UCase(lugarDespacho) = UCase(enumLugarDespachoToString(mEnum)) Then
         strLugarDespachoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strLugarDespachoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & lugarDespacho & " no existe en tipo enumerativo lugarDespacho"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithLugarDespacho(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumLugarDespachoToString(enum_LugarDespacho.eLD_INTERIOR)
      End If
      varComboBox.List(nCount) = enumLugarDespachoToString(enum_LugarDespacho.eLD_INTERIOR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumLugarDespachoToString(enum_LugarDespacho.eLD_CARACAS)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumMotivoDelDespachoToString(ByVal motivoDelDespacho As enum_MotivoDelDespacho) As String
   Select Case motivoDelDespacho
      Case eMD_FACTURA: enumMotivoDelDespachoToString = "Factura"
      Case eMD_Reposicion: enumMotivoDelDespachoToString = "Reposición"
      Case eMD_DEMO: enumMotivoDelDespachoToString = "Demo"
      Case eMD_OTROS: enumMotivoDelDespachoToString = "Otros"
      Case eMD_NOTA_ENTREGA: enumMotivoDelDespachoToString = "Nota Entrega"
      Case eMD_EVENTO: enumMotivoDelDespachoToString = "Evento"
      Case eMD_DEPOSITO_ENVIO: enumMotivoDelDespachoToString = "Depositó Envio 7.500 Bs"
      Case Else: enumMotivoDelDespachoToString = enumMotivoDelDespachoToString(0)
   End Select
End Function

Public Function strMotivoDelDespachoToNum(ByVal motivoDelDespacho As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MotivoDelDespacho
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMD_FACTURA To eMD_DEPOSITO_ENVIO
      If UCase(motivoDelDespacho) = UCase(enumMotivoDelDespachoToString(mEnum)) Then
         strMotivoDelDespachoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMotivoDelDespachoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & motivoDelDespacho & " no existe en tipo enumerativo motivoDelDespacho"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMotivoDelDespacho(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_FACTURA)
   End If
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_FACTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_Reposicion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_DEMO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_OTROS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_NOTA_ENTREGA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_EVENTO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMotivoDelDespachoToString(enum_MotivoDelDespacho.eMD_DEPOSITO_ENVIO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumTipodePersonaRetencionToString(ByVal tipodePersonaRetencion As enum_TipodePersonaRetencion) As String
   Select Case tipodePersonaRetencion
      Case eTP_PJ_DOMICILIADA: enumTipodePersonaRetencionToString = "Jurídica Domiciliada"
      Case eTP_PJ_NODOMICILIADA: enumTipodePersonaRetencionToString = "Jurídica No Domiciliada"
      Case eTP_PN_RESIDENTE: enumTipodePersonaRetencionToString = "Natural Residente"
      Case eTP_PN_NORESIDENTE: enumTipodePersonaRetencionToString = "Natural No Residente"
      Case Else: enumTipodePersonaRetencionToString = enumTipodePersonaRetencionToString(0)
   End Select
End Function

Public Function strTipodePersonaRetencionToNum(ByVal tipodePersonaRetencion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipodePersonaRetencion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTP_PJ_DOMICILIADA To eTP_PN_NORESIDENTE
      If UCase(tipodePersonaRetencion) = UCase(enumTipodePersonaRetencionToString(mEnum)) Then
         strTipodePersonaRetencionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipodePersonaRetencionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipodePersonaRetencion & " no existe en tipo enumerativo tipodePersonaRetencion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipodePersonaRetencion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA)
      End If
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PJ_NODOMICILIADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PN_RESIDENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodePersonaRetencionToString(enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusPagoToString(ByVal statusPago As enum_StatusPago) As String
   Select Case statusPago
      Case eSP_VIGENTE: enumStatusPagoToString = "Vigente"
      Case eSP_ANULADA: enumStatusPagoToString = "Anulada"
      Case Else: enumStatusPagoToString = enumStatusPagoToString(0)
   End Select
End Function

Public Function strStatusPagoToNum(ByVal statusPago As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusPago
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSP_VIGENTE To eSP_ANULADA
      If UCase(statusPago) = UCase(enumStatusPagoToString(mEnum)) Then
         strStatusPagoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusPagoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusPago & " no existe en tipo enumerativo statusPago"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusPago(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusPagoToString(enum_StatusPago.eSP_VIGENTE)
      End If
      varComboBox.List(nCount) = enumStatusPagoToString(enum_StatusPago.eSP_VIGENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusPagoToString(enum_StatusPago.eSP_ANULADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enum_menusDelSistemaAdministrativoToStr(ByVal valMenu As menusDelSistemaAdministrativo) As String
   Select Case valMenu
      Case me_PRINCIPALSAW: enum_menusDelSistemaAdministrativoToStr = "Principal"
      Case me_CXC: enum_menusDelSistemaAdministrativoToStr = "CxC y Cobranza"
      Case me_COMPANIA: enum_menusDelSistemaAdministrativoToStr = "Empresa"
      Case me_TABLASSAW: enum_menusDelSistemaAdministrativoToStr = "Tablas"
      Case me_INVENTARIO: enum_menusDelSistemaAdministrativoToStr = "Inventario"
      Case me_CXP: enum_menusDelSistemaAdministrativoToStr = "CxP y Pagos"
      Case me_BANCO: enum_menusDelSistemaAdministrativoToStr = "Banco"
      Case me_PARAMETROS: enum_menusDelSistemaAdministrativoToStr = "Parámetros"
      Case me_VEHICULOS: enum_menusDelSistemaAdministrativoToStr = "Vehículos"
      Case me_IMPORTAREXPORTAR: enum_menusDelSistemaAdministrativoToStr = "Importar/Exportar"
      Case me_PRINCIPALWINCONT: enum_menusDelSistemaAdministrativoToStr = "Principal Contabilidad"
      Case me_ACTIVOFIJO: enum_menusDelSistemaAdministrativoToStr = "Activo Fijo"
      Case me_TABLASWINCONT: enum_menusDelSistemaAdministrativoToStr = "Tablas Contabilidad"
      Case me_IMPORTEXPORTWINCONT: enum_menusDelSistemaAdministrativoToStr = "Import-Export Contabilidad"
      Case me_CONTABILIZACION: enum_menusDelSistemaAdministrativoToStr = "Contabilización"
      Case me_PRIVATE: enum_menusDelSistemaAdministrativoToStr = "Privado"
      Case me_COMPRAS: enum_menusDelSistemaAdministrativoToStr = "Compras"
      Case me_INFORME_DE_AUDITORIA: enum_menusDelSistemaAdministrativoToStr = "Informe de Auditoría"
      Case me_CONEXIONISLR: enum_menusDelSistemaAdministrativoToStr = "Impuesto sobre la Renta"
      Case me_PRINCIPALCAJA: enum_menusDelSistemaAdministrativoToStr = "Facturación - Caja"
      Case me_CONEXIONAXI: enum_menusDelSistemaAdministrativoToStr = "Ajuste por Inflación"
      Case me_CAJA_CHICA_RENDICION: enum_menusDelSistemaAdministrativoToStr = "Reposición de Caja Chica"
      Case me_ESTRUCTURA_COSTOS: enum_menusDelSistemaAdministrativoToStr = "Estructura de Costos"
      Case me_CONVERSION_RESPALDO_LOTE: enum_menusDelSistemaAdministrativoToStr = "Conversión, Respadar y Lote"
      Case me_TABLASSAW_CONT: enum_menusDelSistemaAdministrativoToStr = "Tablas Continuación"
      Case me_PRODUCCION: enum_menusDelSistemaAdministrativoToStr = "Producción"
      Case Else: enum_menusDelSistemaAdministrativoToStr = enum_menusDelSistemaAdministrativoToStr(0)
   End Select
End Function

Public Function enumTipoDeCtaBancariaToString(ByVal tipoDeCtaBancaria As enum_TipoDeCtaBancaria) As String
   Select Case tipoDeCtaBancaria
      Case eTD_CORRIENTE: enumTipoDeCtaBancariaToString = "Corriente"
      Case eTD_AHORROS: enumTipoDeCtaBancariaToString = "Ahorros"
      Case eTD_ACTIVOS_LIQUIDOS: enumTipoDeCtaBancariaToString = "Activos Líquidos"
      Case Else: enumTipoDeCtaBancariaToString = enumTipoDeCtaBancariaToString(0)
   End Select
End Function

Public Function strTipoDeCtaBancariaToNum(ByVal tipoDeCtaBancaria As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCtaBancaria
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_CORRIENTE To eTD_ACTIVOS_LIQUIDOS
      If UCase(tipoDeCtaBancaria) = UCase(enumTipoDeCtaBancariaToString(mEnum)) Then
         strTipoDeCtaBancariaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCtaBancariaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeCtaBancaria & " no existe en tipo enumerativo tipoDeCtaBancaria"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeCtaBancaria(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeCtaBancariaToString(enum_TipoDeCtaBancaria.eTD_CORRIENTE)
      End If
      varComboBox.List(nCount) = enumTipoDeCtaBancariaToString(enum_TipoDeCtaBancaria.eTD_CORRIENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCtaBancariaToString(enum_TipoDeCtaBancaria.eTD_AHORROS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCtaBancariaToString(enum_TipoDeCtaBancaria.eTD_ACTIVOS_LIQUIDOS)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusCtaBancariaToString(ByVal statusCtaBancaria As enum_StatusCtaBancaria) As String
   Select Case statusCtaBancaria
      Case eSC_ACTIVO: enumStatusCtaBancariaToString = "Activo"
      Case eSC_INACTIVO: enumStatusCtaBancariaToString = "Inactivo"
      Case Else: enumStatusCtaBancariaToString = enumStatusCtaBancariaToString(0)
   End Select
End Function

Public Function strStatusCtaBancariaToNum(ByVal statusCtaBancaria As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCtaBancaria
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_ACTIVO To eSC_INACTIVO
      If UCase(statusCtaBancaria) = UCase(enumStatusCtaBancariaToString(mEnum)) Then
         strStatusCtaBancariaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusCtaBancariaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusCtaBancaria & " no existe en tipo enumerativo statusCtaBancaria"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCtaBancaria(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusCtaBancariaToString(enum_StatusCtaBancaria.eSC_ACTIVA)
      End If
      varComboBox.List(nCount) = enumStatusCtaBancariaToString(enum_StatusCtaBancaria.eSC_ACTIVA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusCtaBancariaToString(enum_StatusCtaBancaria.eSC_INACTIVA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumIngresoEgresoToString(ByVal ingresoEgreso As enum_IngresoEgreso) As String
   Select Case ingresoEgreso
      Case eIE_INGRESO: enumIngresoEgresoToString = "Ingreso"
      Case eIE_EGRESO: enumIngresoEgresoToString = "Egreso"
      Case Else: enumIngresoEgresoToString = enumIngresoEgresoToString(0)
   End Select
End Function

Public Function strIngresoEgresoToNum(ByVal ingresoEgreso As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_IngresoEgreso
   Dim nCount As Integer
   nCount = 0
   For mEnum = eIE_INGRESO To eIE_EGRESO
      If UCase(ingresoEgreso) = UCase(enumIngresoEgresoToString(mEnum)) Then
         strIngresoEgresoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strIngresoEgresoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ingresoEgreso & " no existe en tipo enumerativo ingresoEgreso"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithIngresoEgreso(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumIngresoEgresoToString(enum_IngresoEgreso.eIE_INGRESO)
      End If
      varComboBox.List(nCount) = enumIngresoEgresoToString(enum_IngresoEgreso.eIE_INGRESO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumIngresoEgresoToString(enum_IngresoEgreso.eIE_EGRESO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumGeneradoPorToString(ByVal GeneradoPor As enum_GeneradoPor) As String
   Select Case GeneradoPor
      Case eGP_USUARIO: enumGeneradoPorToString = "Usuario"
      Case eGP_COBRANZA: enumGeneradoPorToString = "Cobranza"
      Case eGP_ORDEN_DE_PAGO: enumGeneradoPorToString = "Orden de Pago"
      Case eGP_CXP: enumGeneradoPorToString = "CxP"
      Case eGP_DEBITO_BANCARIO: enumGeneradoPorToString = "Débito Bancario"
      Case eGP_CXC: enumGeneradoPorToString = "CxC"
      Case eGP_GESTION_COBRANZA: enumGeneradoPorToString = "Gestión de Cobranza"
      Case eGP_COMPRA: enumGeneradoPorToString = "Compra"
      Case eGP_ANTICIPO: enumGeneradoPorToString = "Anticipo"
      Case eGP_RECONVERSION_MONETARIA: enumGeneradoPorToString = "Reconversión Monetaria"
      Case eGP_S10: enumGeneradoPorToString = "Sistema 10"
      Case eGP_RENDICION: enumGeneradoPorToString = "Rendición"
      Case eGP_SOLICITUD_DE_PAGO: enumGeneradoPorToString = "Solicitud de Pago"
      Case eGP_REPOSICION_DE_CAJACHICA: enumGeneradoPorToString = "Caja Chica"
      Case eGP_RETENCION_EN_COBRANZA: enumGeneradoPorToString = "Ret. en Cobranza"
      Case Else: enumGeneradoPorToString = enumGeneradoPorToString(0)
   End Select
End Function
Public Function enumGeneradoPorToSiglas(ByVal GeneradoPor As enum_GeneradoPor) As String
   Select Case GeneradoPor
      Case eGP_USUARIO: enumGeneradoPorToSiglas = "USU"
      Case eGP_COBRANZA: enumGeneradoPorToSiglas = "COB"
      Case eGP_ORDEN_DE_PAGO: enumGeneradoPorToSiglas = "O/P"
      Case eGP_CXP: enumGeneradoPorToSiglas = "CxP"
      Case eGP_DEBITO_BANCARIO: enumGeneradoPorToSiglas = "D/B"
      Case eGP_CXC: enumGeneradoPorToSiglas = "CxC"
      Case eGP_GESTION_COBRANZA: enumGeneradoPorToSiglas = "G/C"
      Case eGP_COMPRA: enumGeneradoPorToSiglas = "Com"
      Case eGP_ANTICIPO: enumGeneradoPorToSiglas = "Ant"
      Case eGP_RECONVERSION_MONETARIA: enumGeneradoPorToSiglas = "R/M"
      Case eGP_S10: enumGeneradoPorToSiglas = "S10"
      Case eGP_RENDICION: enumGeneradoPorToSiglas = "Rnd"
      Case eGP_SOLICITUD_DE_PAGO: enumGeneradoPorToSiglas = "S/P"
      Case eGP_REPOSICION_DE_CAJACHICA: enumGeneradoPorToSiglas = "CCH"
      Case eGP_RETENCION_EN_COBRANZA: enumGeneradoPorToSiglas = "RCB"
      Case Else: enumGeneradoPorToSiglas = enumGeneradoPorToSiglas(0)
   End Select
End Function
Public Function strGeneradoPorToNum(ByVal GeneradoPor As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_GeneradoPor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGP_USUARIO To eGP_RETENCION_EN_COBRANZA
      If UCase(GeneradoPor) = UCase(enumGeneradoPorToString(mEnum)) Then
         strGeneradoPorToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strGeneradoPorToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & GeneradoPor & " no existe en tipo enumerativo generadoPor"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithGeneradoPor(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      varComboBox.Clear
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumGeneradoPorToString(enum_GeneradoPor.eGP_USUARIO)
      End If
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_USUARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_COBRANZA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_ORDEN_DE_PAGO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_CXP)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_CXC)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_GESTION_COBRANZA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_COMPRA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_ANTICIPO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_RECONVERSION_MONETARIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_S10)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_RENDICION)
      nCount = nCount + 1
        varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_REPOSICION_DE_CAJACHICA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_SOLICITUD_DE_PAGO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGeneradoPorToString(enum_GeneradoPor.eGP_RETENCION_EN_COBRANZA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeCompraToString(ByVal tipoDeCompra As enum_TipoDeCompra) As String
   Select Case tipoDeCompra
      Case eTD_COMPRASEXENTAS: enumTipoDeCompraToString = "Exentas"
      Case eTD_COMPRASIMPORTACION: enumTipoDeCompraToString = "Importación"
      Case eTD_COMPRASNACIONALES: enumTipoDeCompraToString = "Internas"
      Case eTD_COMPRASEXONERADAS: enumTipoDeCompraToString = "Exoneradas"
      Case eTD_COMPRASNOSUJETAS: enumTipoDeCompraToString = "No sujetas al Impuesto"
      Case eTD_COMPRASSINDERECHOACREDITO: enumTipoDeCompraToString = "Sin derecho a crédito"
      Case Else: enumTipoDeCompraToString = enumTipoDeCompraToString(0)
   End Select
End Function

Public Function strTipoDeCompraToNum(ByVal tipoDeCompra As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCompra
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_COMPRASEXENTAS To eTD_COMPRASSINDERECHOACREDITO
      If UCase(tipoDeCompra) = UCase(enumTipoDeCompraToString(mEnum)) Then
         strTipoDeCompraToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCompraToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeCompra & " no existe en tipo enumerativo tipoDeCompra"
   End If
h_EXIT:
   Exit Function
End Function
Public Function strSiglasTipoDeCompraToNum(ByVal tipoDeCompra As String) As Integer
   Dim mEnum As enum_TipoDeCompra
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_COMPRASEXENTAS To eTD_COMPRASSINDERECHOACREDITO
      If UCase(tipoDeCompra) = UCase(enumTipoDeCompraToStringSiglas(mEnum)) Then
         strSiglasTipoDeCompraToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strSiglasTipoDeCompraToNum = 0
h_EXIT:
   Exit Function
End Function
Public Function strSiglasTipoDeCompraToString(ByVal tipoDeCompra As String) As String
   Dim mEnum As enum_TipoDeCompra
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_COMPRASEXENTAS To eTD_COMPRASSINDERECHOACREDITO
      If UCase(tipoDeCompra) = UCase(enumTipoDeCompraToStringSiglas(mEnum)) Then
         strSiglasTipoDeCompraToString = enumTipoDeCompraToString(mEnum)
         GoTo h_EXIT
      End If
   Next
   strSiglasTipoDeCompraToString = 0
h_EXIT:
   Exit Function
End Function

Public Function strTipoDeCompraToNumBoolean(ByVal tipoDeCompra As String) As Boolean
   Dim mEnum As enum_TipoDeCompra
   Dim nCount As Integer
   nCount = 0
   strTipoDeCompraToNumBoolean = False
   For mEnum = eTD_COMPRASEXENTAS To eTD_COMPRASSINDERECHOACREDITO
      If UCase(tipoDeCompra) = UCase(enumTipoDeCompraToString(mEnum)) Then
         strTipoDeCompraToNum True
      End If
   Next
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeCompra(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASEXENTAS)
      End If
      varComboBox.List(nCount) = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASEXENTAS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASIMPORTACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASNACIONALES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASEXONERADAS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASNOSUJETAS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeCompraToString(enum_TipoDeCompra.eTD_COMPRASSINDERECHOACREDITO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = 2  ' Se selecciona el valor por defecto
      End If
   End If
End Sub

Public Function enumCreditoFiscalToString(ByVal CreditoFiscal As enum_CreditoFiscal) As String
   If gGlobalization.fEsCodigoVenezuela Then
      Select Case CreditoFiscal
         Case eCF_DEDUCIBLE: enumCreditoFiscalToString = "Deducible"
         Case eCF_PRORRATEABLE: enumCreditoFiscalToString = "Prorrateable"
         Case eCF_NoDeducible: enumCreditoFiscalToString = "No Deducible"
         Case Else: enumCreditoFiscalToString = enumCreditoFiscalToString(0)
      End Select
   ElseIf gGlobalization.fEsCodigoPeru Then
      Select Case CreditoFiscal
         Case eCF_DEDUCIBLE: enumCreditoFiscalToString = "Gravadas"
         Case eCF_PRORRATEABLE: enumCreditoFiscalToString = "Gravadas/No Gravadas"
         Case eCF_NoDeducible: enumCreditoFiscalToString = "No Gravadas"
         Case Else: enumCreditoFiscalToString = enumCreditoFiscalToString(0)
      End Select
   End If
End Function
Public Function enumCreditoFiscalToSiglas(ByVal CreditoFiscal As enum_CreditoFiscal) As String
   Select Case CreditoFiscal
      Case eCF_DEDUCIBLE: enumCreditoFiscalToSiglas = "DDU"
      Case eCF_PRORRATEABLE: enumCreditoFiscalToSiglas = "PRO"
      Case eCF_NoDeducible: enumCreditoFiscalToSiglas = "N/D"
      Case Else: enumCreditoFiscalToSiglas = enumCreditoFiscalToSiglas(0)
   End Select
End Function

Public Function strCreditoFiscalToNum(ByVal CreditoFiscal As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_CreditoFiscal
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCF_DEDUCIBLE To eCF_NoDeducible
      If UCase(CreditoFiscal) = UCase(enumCreditoFiscalToString(mEnum)) Then
         strCreditoFiscalToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCreditoFiscalToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & CreditoFiscal & " no existe en tipo enumerativo creditoFiscal"
   End If
h_EXIT:
   Exit Function
End Function
Public Function strSiglasCreditoFiscalToNum(ByVal CreditoFiscal As String) As Integer
   Dim mEnum As enum_CreditoFiscal
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCF_DEDUCIBLE To eCF_NoDeducible
      If UCase(CreditoFiscal) = UCase(enumCreditoFiscalToSiglas(mEnum)) Then
         strSiglasCreditoFiscalToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strSiglasCreditoFiscalToNum = 0
h_EXIT:
   Exit Function
End Function
Public Function strSiglasCreditoFiscalToString(ByVal CreditoFiscal As String) As String
   Dim mEnum As enum_TipoDeCompra
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCF_DEDUCIBLE To eCF_NoDeducible
      If UCase(CreditoFiscal) = UCase(enumCreditoFiscalToSiglas(mEnum)) Then
         strSiglasCreditoFiscalToString = enumCreditoFiscalToString(mEnum)
         GoTo h_EXIT
      End If
   Next
   strSiglasCreditoFiscalToString = enumCreditoFiscalToString(eCF_DEDUCIBLE)
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithCreditoFiscal(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumCreditoFiscalToString(enum_CreditoFiscal.eCF_DEDUCIBLE)
      End If
      varComboBox.List(nCount) = enumCreditoFiscalToString(enum_CreditoFiscal.eCF_DEDUCIBLE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCreditoFiscalToString(enum_CreditoFiscal.eCF_PRORRATEABLE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCreditoFiscalToString(enum_CreditoFiscal.eCF_NoDeducible)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumFormaDeCancelarPagoToString(ByVal formaDeCancelarPago As enum_FormaDeCancelarPago) As String
   Select Case formaDeCancelarPago
      Case eFD_EFECTIVO: enumFormaDeCancelarPagoToString = "Efectivo"
      Case eFD_CHEQUE: enumFormaDeCancelarPagoToString = "Cheque"
      Case eFD_TARJETA: enumFormaDeCancelarPagoToString = "Tarjeta"
      Case eFD_DEPOSITO: enumFormaDeCancelarPagoToString = "Depósito"
      Case Else: enumFormaDeCancelarPagoToString = enumFormaDeCancelarPagoToString(0)
   End Select
End Function

Public Function strFormaDeCancelarPagoToNum(ByVal formaDeCancelarPago As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeCancelarPago
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_EFECTIVO To eFD_DEPOSITO
      If UCase(formaDeCancelarPago) = UCase(enumFormaDeCancelarPagoToString(mEnum)) Then
         strFormaDeCancelarPagoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeCancelarPagoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeCancelarPago & " no existe en tipo enumerativo formaDeCancelarPago"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeCancelarPago(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumFormaDeCancelarPagoToString(enum_FormaDeCancelarPago.eFD_EFECTIVO)
      End If
      varComboBox.List(nCount) = enumFormaDeCancelarPagoToString(enum_FormaDeCancelarPago.eFD_EFECTIVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeCancelarPagoToString(enum_FormaDeCancelarPago.eFD_CHEQUE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeCancelarPagoToString(enum_FormaDeCancelarPago.eFD_TARJETA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeCancelarPagoToString(enum_FormaDeCancelarPago.eFD_DEPOSITO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumHorarioToString(ByVal horario As enum_Horario) As String
   Select Case horario
      Case eHo_DE_800_AM_A_1200_PM: enumHorarioToString = "de 8 am a 12 pm"
      Case eHo_DE_900_AM_A_1130_AM: enumHorarioToString = "de 9 am a 11:30 am"
      Case eHo_DE_130_PM_A_430_PM: enumHorarioToString = "de 1:30 pm a 4:30 pm"
      Case eHo_DE_130_PM_A_530_PM: enumHorarioToString = "de 1:30 pm a 5:30 pm"
      Case eHo_DE_1_PM_A_530_PM: enumHorarioToString = "de 1 pm a 5:30 pm"
      Case eHo_DE_2_PM_A_530_PM: enumHorarioToString = "de 2 pm a 5:30 pm"
      Case eHo_DE_800_AM_A_530_PM: enumHorarioToString = "de 8 a 12m y de 2 a 5:30pm"
      Case Else: enumHorarioToString = enumHorarioToString(0)
   End Select
End Function

Public Function strHorarioToNum(ByVal horario As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Horario
   Dim nCount As Integer
   nCount = 0
   For mEnum = eHo_DE_800_AM_A_1200_PM To eHo_DE_800_AM_A_530_PM
      If UCase(horario) = UCase(enumHorarioToString(mEnum)) Then
         strHorarioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strHorarioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & horario & " no existe en tipo enumerativo horario"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithHorario(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumHorarioToString(enum_Horario.eHo_DE_800_AM_A_1200_PM)
   End If
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_800_AM_A_1200_PM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_900_AM_A_1130_AM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_130_PM_A_430_PM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_130_PM_A_530_PM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_1_PM_A_530_PM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_2_PM_A_530_PM)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHorarioToString(enum_Horario.eHo_DE_800_AM_A_530_PM)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumStatusParticipanteToString(ByVal statusParticipante As enum_StatusParticipante) As String
   Select Case statusParticipante
      Case eSP_RESERVADO: enumStatusParticipanteToString = "Reservado"
      Case eSP_CONFIRMADO: enumStatusParticipanteToString = "Confirmado"
      Case eSP_ASISTIO: enumStatusParticipanteToString = "Asistió"
      Case eSP_CANCELADO: enumStatusParticipanteToString = "Cancelado"
      Case eSP_NO_ASISTIO: enumStatusParticipanteToString = "No Asistió"
      Case eSP_LISTA_ESPERA: enumStatusParticipanteToString = "Lista de Espera"
      Case eSP_TRASLADADO: enumStatusParticipanteToString = "Trasladado"
      Case Else: enumStatusParticipanteToString = enumStatusParticipanteToString(0)
   End Select
End Function

Public Function strStatusParticipanteToNum(ByVal statusParticipante As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusParticipante
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSP_RESERVADO To eSP_TRASLADADO
      If UCase(statusParticipante) = UCase(enumStatusParticipanteToString(mEnum)) Then
         strStatusParticipanteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusParticipanteToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusParticipante & " no existe en tipo enumerativo statusParticipante"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusParticipante(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusParticipanteToString(enum_StatusParticipante.eSP_RESERVADO)
   End If
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_RESERVADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_CONFIRMADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_ASISTIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_CANCELADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_NO_ASISTIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_LISTA_ESPERA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusParticipanteToString(enum_StatusParticipante.eSP_TRASLADADO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function enumBaseCalculoOtrosCargosDeFacturaToString(ByVal baseCalculoOtrosCargosDeFactura As enum_BaseCalculoOtrosCargosDeFactura) As String
   Select Case baseCalculoOtrosCargosDeFactura
      Case eBC_FORMULA: enumBaseCalculoOtrosCargosDeFacturaToString = "Fórmula"
      Case eBC_MONTO_FIJO: enumBaseCalculoOtrosCargosDeFacturaToString = "Monto Fijo"
      Case eBC_INDICAR_MONTO: enumBaseCalculoOtrosCargosDeFacturaToString = "Indicar Monto"
      Case Else: enumBaseCalculoOtrosCargosDeFacturaToString = enumBaseCalculoOtrosCargosDeFacturaToString(0)
   End Select
End Function

Public Function strBaseCalculoOtrosCargosDeFacturaToNum(ByVal baseCalculoOtrosCargosDeFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_BaseCalculoOtrosCargosDeFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eBC_FORMULA To eBC_INDICAR_MONTO
      If UCase(baseCalculoOtrosCargosDeFactura) = UCase(enumBaseCalculoOtrosCargosDeFacturaToString(mEnum)) Then
         strBaseCalculoOtrosCargosDeFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strBaseCalculoOtrosCargosDeFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & baseCalculoOtrosCargosDeFactura & " no existe en tipo enumerativo baseCalculoOtrosCargosDeFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithBaseCalculoOtrosCargosDeFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumBaseCalculoOtrosCargosDeFacturaToString(enum_BaseCalculoOtrosCargosDeFactura.eBC_FORMULA)
      End If
      varComboBox.List(nCount) = enumBaseCalculoOtrosCargosDeFacturaToString(enum_BaseCalculoOtrosCargosDeFactura.eBC_FORMULA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumBaseCalculoOtrosCargosDeFacturaToString(enum_BaseCalculoOtrosCargosDeFactura.eBC_MONTO_FIJO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumBaseCalculoOtrosCargosDeFacturaToString(enum_BaseCalculoOtrosCargosDeFactura.eBC_INDICAR_MONTO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumBaseFormulaOtrosCargosDeFacturaToString(ByVal baseFormulaOtrosCargosDeFactura As enum_BaseFormulaOtrosCargosDeFactura) As String
   Select Case baseFormulaOtrosCargosDeFactura
      Case eBF_SUBTOTAL: enumBaseFormulaOtrosCargosDeFacturaToString = "Sub-Total"
      Case eBF_BASE_IMPONIBLE: enumBaseFormulaOtrosCargosDeFacturaToString = "Base Imponible"
      Case eBF_TOTALFACTURA: enumBaseFormulaOtrosCargosDeFacturaToString = "Total Factura"
      Case Else: enumBaseFormulaOtrosCargosDeFacturaToString = enumBaseFormulaOtrosCargosDeFacturaToString(0)
   End Select
End Function

Public Function strBaseFormulaOtrosCargosDeFacturaToNum(ByVal baseFormulaOtrosCargosDeFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_BaseFormulaOtrosCargosDeFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eBF_SUBTOTAL To eBF_TOTALFACTURA
      If UCase(baseFormulaOtrosCargosDeFactura) = UCase(enumBaseFormulaOtrosCargosDeFacturaToString(mEnum)) Then
         strBaseFormulaOtrosCargosDeFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strBaseFormulaOtrosCargosDeFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & baseFormulaOtrosCargosDeFactura & " no existe en tipo enumerativo baseFormulaOtrosCargosDeFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithBaseFormulaOtrosCargosDeFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumBaseFormulaOtrosCargosDeFacturaToString(enum_BaseFormulaOtrosCargosDeFactura.eBF_SUBTOTAL)
      End If
      varComboBox.List(nCount) = enumBaseFormulaOtrosCargosDeFacturaToString(enum_BaseFormulaOtrosCargosDeFactura.eBF_SUBTOTAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumBaseFormulaOtrosCargosDeFacturaToString(enum_BaseFormulaOtrosCargosDeFactura.eBF_BASE_IMPONIBLE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumBaseFormulaOtrosCargosDeFacturaToString(enum_BaseFormulaOtrosCargosDeFactura.eBF_TOTALFACTURA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumComoAplicaOtrosCargosDeFacturaToString(ByVal comoAplicaOtrosCargosDeFactura As enum_ComoAplicaOtrosCargosDeFactura) As String
   Select Case comoAplicaOtrosCargosDeFactura
      Case eCA_SUMA: enumComoAplicaOtrosCargosDeFacturaToString = "Suma"
      Case eCA_RESTA: enumComoAplicaOtrosCargosDeFacturaToString = "Resta"
      Case eCA_NO_APLICA_SOLO_INFORMATIVO: enumComoAplicaOtrosCargosDeFacturaToString = "No Aplica (solo informativo)"
      Case Else: enumComoAplicaOtrosCargosDeFacturaToString = enumComoAplicaOtrosCargosDeFacturaToString(0)
   End Select
End Function

Public Function strComoAplicaOtrosCargosDeFacturaToNum(ByVal comoAplicaOtrosCargosDeFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ComoAplicaOtrosCargosDeFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCA_SUMA To eCA_NO_APLICA_SOLO_INFORMATIVO
      If UCase(comoAplicaOtrosCargosDeFactura) = UCase(enumComoAplicaOtrosCargosDeFacturaToString(mEnum)) Then
         strComoAplicaOtrosCargosDeFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strComoAplicaOtrosCargosDeFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & comoAplicaOtrosCargosDeFactura & " no existe en tipo enumerativo comoAplicaOtrosCargosDeFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithComoAplicaOtrosCargosDeFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumComoAplicaOtrosCargosDeFacturaToString(enum_ComoAplicaOtrosCargosDeFactura.eCA_SUMA)
      End If
      varComboBox.List(nCount) = enumComoAplicaOtrosCargosDeFacturaToString(enum_ComoAplicaOtrosCargosDeFactura.eCA_SUMA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumComoAplicaOtrosCargosDeFacturaToString(enum_ComoAplicaOtrosCargosDeFactura.eCA_RESTA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumComoAplicaOtrosCargosDeFacturaToString(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusContratoToString(ByVal statusContrato As enum_StatusContrato) As String
   Select Case statusContrato
      Case eSCo_VIGENTE: enumStatusContratoToString = "Vigente"
      Case eSCo_DESACTIVADO: enumStatusContratoToString = "Desactivado"
      Case Else: enumStatusContratoToString = enumStatusContratoToString(0)
   End Select
End Function

Public Function strStatusContratoToNum(ByVal statusContrato As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusContrato
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSCo_VIGENTE To eSCo_DESACTIVADO
      If UCase(statusContrato) = UCase(enumStatusContratoToString(mEnum)) Then
         strStatusContratoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusContratoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusContrato & " no existe en tipo enumerativo statusContrato"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusContrato(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusContratoToString(enum_StatusContrato.eSCo_VIGENTE)
      End If
      varComboBox.List(nCount) = enumStatusContratoToString(enum_StatusContrato.eSCo_VIGENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusContratoToString(enum_StatusContrato.eSCo_DESACTIVADO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumDuracionDelContratoToString(ByVal duracionDelContrato As enum_DuracionDelContrato) As String
   Select Case duracionDelContrato
      Case eDD_DURACIONFIJA: enumDuracionDelContratoToString = "Duración Fija"
      Case eDD_DURACIONINDEFINIDA: enumDuracionDelContratoToString = "Duración indefinida"
      Case Else: enumDuracionDelContratoToString = enumDuracionDelContratoToString(0)
   End Select
End Function

Public Function strDuracionDelContratoToNum(ByVal duracionDelContrato As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_DuracionDelContrato
   Dim nCount As Integer
   nCount = 0
   For mEnum = eDD_DURACIONFIJA To eDD_DURACIONINDEFINIDA
      If UCase(duracionDelContrato) = UCase(enumDuracionDelContratoToString(mEnum)) Then
         strDuracionDelContratoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strDuracionDelContratoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & duracionDelContrato & " no existe en tipo enumerativo duracionDelContrato"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithDuracionDelContrato(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumDuracionDelContratoToString(enum_DuracionDelContrato.eDD_DURACIONFIJA)
      End If
      varComboBox.List(nCount) = enumDuracionDelContratoToString(enum_DuracionDelContrato.eDD_DURACIONFIJA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDuracionDelContratoToString(enum_DuracionDelContrato.eDD_DURACIONINDEFINIDA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumValorDelRenglonToString(ByVal valorDelRenglon As enum_ValorDelRenglon) As String
   Select Case valorDelRenglon
      Case eVD_VALORENARCHIVOARTICULOS: enumValorDelRenglonToString = "Valor del Archivo"
      Case eVD_INDICARELVALOR: enumValorDelRenglonToString = "Indicar el valor"
      Case Else: enumValorDelRenglonToString = enumValorDelRenglonToString(0)
   End Select
End Function

Public Function strValorDelRenglonToNum(ByVal valorDelRenglon As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ValorDelRenglon
   Dim nCount As Integer
   nCount = 0
   For mEnum = eVD_VALORENARCHIVOARTICULOS To eVD_INDICARELVALOR
      If UCase(valorDelRenglon) = UCase(enumValorDelRenglonToString(mEnum)) Then
         strValorDelRenglonToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strValorDelRenglonToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valorDelRenglon & " no existe en tipo enumerativo valorDelRenglon"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithValorDelRenglon(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumValorDelRenglonToString(enum_ValorDelRenglon.eVD_VALORENARCHIVOARTICULOS)
      End If
      varComboBox.List(nCount) = enumValorDelRenglonToString(enum_ValorDelRenglon.eVD_VALORENARCHIVOARTICULOS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumValorDelRenglonToString(enum_ValorDelRenglon.eVD_INDICARELVALOR)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumPeriodicidadContratosToString(ByVal periodicidadContratos As enum_PeriodicidadContratos) As String
   Select Case periodicidadContratos
      Case ePC_MENSUAL: enumPeriodicidadContratosToString = "Mensual"
      Case ePC_BIMESTRAL: enumPeriodicidadContratosToString = "Bimestral"
      Case ePC_TRIMESTRAL: enumPeriodicidadContratosToString = "Trimestral"
      Case ePC_SEMESTRAL: enumPeriodicidadContratosToString = "Semestral"
      Case ePC_ANUAL: enumPeriodicidadContratosToString = "Anual"
      Case ePC_CUOTAUNICA: enumPeriodicidadContratosToString = "Cuota única"
      Case Else: enumPeriodicidadContratosToString = enumPeriodicidadContratosToString(0)
   End Select
End Function

Public Function enumOrdinalToPeriodicidadContratos(ByVal valOrdinalPeriodicidad As Integer) As enum_PeriodicidadContratos
   Select Case valOrdinalPeriodicidad
      Case 0: enumOrdinalToPeriodicidadContratos = ePC_MENSUAL
      Case 1: enumOrdinalToPeriodicidadContratos = ePC_BIMESTRAL
      Case 2: enumOrdinalToPeriodicidadContratos = ePC_TRIMESTRAL
      Case 3: enumOrdinalToPeriodicidadContratos = ePC_SEMESTRAL
      Case 4: enumOrdinalToPeriodicidadContratos = ePC_ANUAL
      Case 5: enumOrdinalToPeriodicidadContratos = ePC_CUOTAUNICA
      Case Else: enumOrdinalToPeriodicidadContratos = ePC_MENSUAL
   End Select
End Function

Public Function strPeriodicidadContratosToNum(ByVal periodicidadContratos As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_PeriodicidadContratos
   Dim nCount As Integer
   nCount = 0
   For mEnum = ePC_MENSUAL To ePC_CUOTAUNICA
      If UCase(periodicidadContratos) = UCase(enumPeriodicidadContratosToString(mEnum)) Then
         strPeriodicidadContratosToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strPeriodicidadContratosToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & periodicidadContratos & " no existe en tipo enumerativo periodicidadContratos"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithPeriodicidadContratos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_MENSUAL)
      End If
      varComboBox.List(nCount) = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_MENSUAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_BIMESTRAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_TRIMESTRAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_SEMESTRAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_ANUAL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPeriodicidadContratosToString(enum_PeriodicidadContratos.ePC_CUOTAUNICA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function enumPeriodoDeAplicacionToString(ByVal periodoDeAplicacion As enum_PeriodoDeAplicacion) As String
   Select Case periodoDeAplicacion
      Case ePD_ELDELCONTRATO: enumPeriodoDeAplicacionToString = "El del Contrato"
      Case ePD_ESPECIALINDICAR: enumPeriodoDeAplicacionToString = "Especial (Indicar)"
      Case Else: enumPeriodoDeAplicacionToString = enumPeriodoDeAplicacionToString(0)
   End Select
End Function

Public Function strPeriodoDeAplicacionToNum(ByVal periodoDeAplicacion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_PeriodoDeAplicacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = ePD_ELDELCONTRATO To ePD_ESPECIALINDICAR
      If UCase(periodoDeAplicacion) = UCase(enumPeriodoDeAplicacionToString(mEnum)) Then
         strPeriodoDeAplicacionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strPeriodoDeAplicacionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & periodoDeAplicacion & " no existe en tipo enumerativo periodoDeAplicacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithPeriodoDeAplicacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumPeriodoDeAplicacionToString(enum_PeriodoDeAplicacion.ePD_ELDELCONTRATO)
      End If
      varComboBox.List(nCount) = enumPeriodoDeAplicacionToString(enum_PeriodoDeAplicacion.ePD_ELDELCONTRATO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPeriodoDeAplicacionToString(enum_PeriodoDeAplicacion.ePD_ESPECIALINDICAR)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusCursoToString(ByVal statusCurso As enum_StatusCurso) As String
   Select Case statusCurso
      Case eSC_DISPONIBLE: enumStatusCursoToString = "Disponible"
      Case eSC_LLENO: enumStatusCursoToString = "Lleno"
      Case eSC_CANCELADO: enumStatusCursoToString = "Cancelado"
      Case eSC_DICTADO: enumStatusCursoToString = "Dictado"
      Case Else: enumStatusCursoToString = enumStatusCursoToString(0)
   End Select
End Function

Public Function strStatusCursoToNum(ByVal statusCurso As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCurso
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_DISPONIBLE To eSC_DICTADO
      If UCase(statusCurso) = UCase(enumStatusCursoToString(mEnum)) Then
         strStatusCursoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusCursoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusCurso & " no existe en tipo enumerativo statusCurso"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCurso(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusCursoToString(enum_StatusCurso.eSC_DISPONIBLE)
      End If
      varComboBox.List(nCount) = enumStatusCursoToString(enum_StatusCurso.eSC_DISPONIBLE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusCursoToString(enum_StatusCurso.eSC_LLENO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusCursoToString(enum_StatusCurso.eSC_CANCELADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusCursoToString(enum_StatusCurso.eSC_DICTADO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub


Public Function enumTipodePersonaRetencionToString4(tipodePersonaRetencion As enum_TipodePersonaRetencion) As String
   Select Case tipodePersonaRetencion
      Case eTP_PJ_DOMICILIADA: enumTipodePersonaRetencionToString4 = "PJD"
      Case eTP_PJ_NODOMICILIADA: enumTipodePersonaRetencionToString4 = "PJND"
      Case eTP_PN_RESIDENTE: enumTipodePersonaRetencionToString4 = "PNR"
      Case eTP_PN_NORESIDENTE: enumTipodePersonaRetencionToString4 = "PNNR"
      Case Else: enumTipodePersonaRetencionToString4 = enumTipodePersonaRetencionToString4(0)
   End Select
End Function

Public Function enumTipoDeCompraToStringSiglas(tipoDeCompra As enum_TipoDeCompra) As String
   Select Case tipoDeCompra
      Case eTD_COMPRASEXENTAS: enumTipoDeCompraToStringSiglas = "C/E"
      Case eTD_COMPRASIMPORTACION: enumTipoDeCompraToStringSiglas = "CIM"
      Case eTD_COMPRASNACIONALES: enumTipoDeCompraToStringSiglas = "CIN"
      Case eTD_COMPRASEXONERADAS: enumTipoDeCompraToStringSiglas = "EXO"
      Case eTD_COMPRASNOSUJETAS: enumTipoDeCompraToStringSiglas = "NSI"
      Case eTD_COMPRASSINDERECHOACREDITO: enumTipoDeCompraToStringSiglas = "SDC"
      Case Else: enumTipoDeCompraToStringSiglas = enumTipoDeCompraToStringSiglas(0)
   End Select
End Function

Public Function enumCantidadAgenerarToString(ByVal cantidadAgenerar As enum_CantidadAgenerar) As String
   Select Case cantidadAgenerar
      Case eCA_TODOSLOSCONTRATOS: enumCantidadAgenerarToString = "Todos los Contratos"
      Case eCA_UNCONTRATO: enumCantidadAgenerarToString = "Un Contrato"
      Case Else: enumCantidadAgenerarToString = enumCantidadAgenerarToString(0)
   End Select
End Function

Public Function strCantidadAgenerarToNum(ByVal cantidadAgenerar As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_CantidadAgenerar
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCA_TODOSLOSCONTRATOS To eCA_UNCONTRATO
      If UCase(cantidadAgenerar) = UCase(enumCantidadAgenerarToString(mEnum)) Then
         strCantidadAgenerarToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCantidadAgenerarToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & cantidadAgenerar & " no existe en tipo enumerativo cantidadAgenerar"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCantidadAgenerar(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumCantidadAgenerarToString(enum_CantidadAgenerar.eCA_TODOSLOSCONTRATOS)
      End If
      varComboBox.List(nCount) = enumCantidadAgenerarToString(enum_CantidadAgenerar.eCA_TODOSLOSCONTRATOS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCantidadAgenerarToString(enum_CantidadAgenerar.eCA_UNCONTRATO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumRedondeaOtruncaToString(ByVal redondeaOtrunca As enum_RedondeaOtrunca) As String
   Select Case redondeaOtrunca
      Case eRO_REDONDEA: enumRedondeaOtruncaToString = "Redondea"
      Case eRO_TRUNCA: enumRedondeaOtruncaToString = "Trunca"
      Case Else: enumRedondeaOtruncaToString = enumRedondeaOtruncaToString(0)
   End Select
End Function

Public Function strRedondeaOtruncaToNum(ByVal redondeaOtrunca As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_RedondeaOtrunca
   Dim nCount As Integer
   nCount = 0
   For mEnum = eRO_REDONDEA To eRO_TRUNCA
      If UCase(redondeaOtrunca) = UCase(enumRedondeaOtruncaToString(mEnum)) Then
         strRedondeaOtruncaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strRedondeaOtruncaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & redondeaOtrunca & " no existe en tipo enumerativo redondeaOtrunca"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithRedondeaOtrunca(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumRedondeaOtruncaToString(enum_RedondeaOtrunca.eRO_REDONDEA)
      End If
      varComboBox.List(nCount) = enumRedondeaOtruncaToString(enum_RedondeaOtrunca.eRO_REDONDEA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumRedondeaOtruncaToString(enum_RedondeaOtrunca.eRO_TRUNCA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumModeloDeFacturaToString(ByVal modeloDeFactura As enum_ModeloDeFactura) As String
   Select Case modeloDeFactura
      Case eMD_FORMALIBRE: enumModeloDeFacturaToString = "Forma Libre (Carta)"
      Case eMD_OTRO: enumModeloDeFacturaToString = "Otro"
      Case eMD_IMPRESION_MODO_TEXTO: enumModeloDeFacturaToString = "Impresion Modo Texto"
      Case Else: enumModeloDeFacturaToString = enumModeloDeFacturaToString(0)
   End Select
End Function

Public Function strModeloDeFacturaToNum(ByVal modeloDeFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ModeloDeFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMD_FORMALIBRE To eMD_IMPRESION_MODO_TEXTO
      If UCase(modeloDeFactura) = UCase(enumModeloDeFacturaToString(mEnum)) Then
         strModeloDeFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strModeloDeFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & modeloDeFactura & " no existe en tipo enumerativo modeloDeFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithModeloDeFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumModeloDeFacturaToString(enum_ModeloDeFactura.eMD_FORMALIBRE)
      End If
      varComboBox.List(nCount) = enumModeloDeFacturaToString(enum_ModeloDeFactura.eMD_FORMALIBRE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumModeloDeFacturaToString(enum_ModeloDeFactura.eMD_OTRO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumModeloDeFacturaToString(enum_ModeloDeFactura.eMD_IMPRESION_MODO_TEXTO)
      varComboBox.Width = gApi.maxLengthOfValuesOfComboBox(varComboBox) * 100 'Numero estimado aproximado
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      End If
   End If
End Sub

Public Function enumTipoDePrefijoToString(ByVal tipoDePrefijo As enum_TipoDePrefijo) As String
   Select Case tipoDePrefijo
      Case eTD_SINPREFIJO: enumTipoDePrefijoToString = "Sin Prefijo"
      Case eTD_ANO: enumTipoDePrefijoToString = "Año"
      Case eTD_INDICAR: enumTipoDePrefijoToString = "Indicar"
      Case Else: enumTipoDePrefijoToString = enumTipoDePrefijoToString(0)
   End Select
End Function

Public Function strTipoDePrefijoToNum(ByVal tipoDePrefijo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDePrefijo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_SINPREFIJO To eTD_INDICAR
      If UCase(tipoDePrefijo) = UCase(enumTipoDePrefijoToString(mEnum)) Then
         strTipoDePrefijoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDePrefijoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDePrefijo & " no existe en tipo enumerativo tipoDePrefijo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDePrefijo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDePrefijoToString(enum_TipoDePrefijo.eTD_SINPREFIJO)
      End If
      varComboBox.List(nCount) = enumTipoDePrefijoToString(enum_TipoDePrefijo.eTD_SINPREFIJO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDePrefijoToString(enum_TipoDePrefijo.eTD_ANO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDePrefijoToString(enum_TipoDePrefijo.eTD_INDICAR)
      varComboBox.Width = gApi.maxLengthOfValuesOfComboBox(varComboBox) * 100 + 100
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumPreciosDeLosProductosToString(ByVal preciosDeLosProductos As enum_PreciosDeLosProductos, ByRef gGlobalization As Object) As String
   Select Case preciosDeLosProductos
      Case ePD_PRECIOCONIVA: enumPreciosDeLosProductosToString = "Precio Con " & gGlobalization.fPromptIVA
      Case ePD_PRECIOSINIVA: enumPreciosDeLosProductosToString = "Precio Sin " & gGlobalization.fPromptIVA
      Case Else: enumPreciosDeLosProductosToString = enumPreciosDeLosProductosToString(0, gGlobalization)
   End Select
End Function

Public Function strPreciosDeLosProductosToNum(ByVal preciosDeLosProductos As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_PreciosDeLosProductos
   Dim nCount As Integer
   nCount = 0
   For mEnum = ePD_PRECIOCONIVA To ePD_PRECIOSINIVA
      If UCase(preciosDeLosProductos) = UCase(enumPreciosDeLosProductosToString(mEnum, gGlobalization)) Then
         strPreciosDeLosProductosToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strPreciosDeLosProductosToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & preciosDeLosProductos & " no existe en tipo enumerativo preciosDeLosProductos"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithPreciosDeLosProductos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumPreciosDeLosProductosToString(enum_PreciosDeLosProductos.ePD_PRECIOCONIVA, gGlobalization)
      End If
      varComboBox.List(nCount) = enumPreciosDeLosProductosToString(enum_PreciosDeLosProductos.ePD_PRECIOCONIVA, gGlobalization)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPreciosDeLosProductosToString(enum_PreciosDeLosProductos.ePD_PRECIOSINIVA, gGlobalization)
      varComboBox.Width = gApi.maxLengthOfValuesOfComboBox(varComboBox) * 100 + 100
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumFormaDeSeleccionarCompaniaToString(ByVal formaDeSeleccionarCompania As enum_FormaDeSeleccionarCompania) As String
   Select Case formaDeSeleccionarCompania
      Case eFD_ESCOGERCOMPANIAALENTRAR: enumFormaDeSeleccionarCompaniaToString = "Escoger una Empresa al entrar"
      Case eFD_ESCOGERULTIMACOMPANIAUSADA: enumFormaDeSeleccionarCompaniaToString = "Escoger la última Empresa usada"
      Case Else: enumFormaDeSeleccionarCompaniaToString = enumFormaDeSeleccionarCompaniaToString(0)
   End Select
End Function

Public Function strFormaDeSeleccionarCompaniaToNum(ByVal formaDeSeleccionarCompania As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeSeleccionarCompania
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_ESCOGERCOMPANIAALENTRAR To eFD_ESCOGERULTIMACOMPANIAUSADA
      If UCase(formaDeSeleccionarCompania) = UCase(enumFormaDeSeleccionarCompaniaToString(mEnum)) Then
         strFormaDeSeleccionarCompaniaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeSeleccionarCompaniaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeSeleccionarCompania & " no existe en tipo enumerativo formaDeSeleccionarCompania"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeSeleccionarCompania(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumFormaDeSeleccionarCompaniaToString(enum_FormaDeSeleccionarCompania.eFD_ESCOGERCOMPANIAALENTRAR)
      End If
      varComboBox.List(nCount) = enumFormaDeSeleccionarCompaniaToString(enum_FormaDeSeleccionarCompania.eFD_ESCOGERCOMPANIAALENTRAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeSeleccionarCompaniaToString(enum_FormaDeSeleccionarCompania.eFD_ESCOGERULTIMACOMPANIAUSADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusPreguntaToString(ByVal statusPregunta As enum_StatusPregunta) As String
   Select Case statusPregunta
      Case eSP_POR_RESPONDER: enumStatusPreguntaToString = "Por Responder"
      Case eSP_RESPONDIDA: enumStatusPreguntaToString = "Respondida"
      Case eSP_FALTA_INFORMACION: enumStatusPreguntaToString = "Falta Información"
      Case Else: enumStatusPreguntaToString = enumStatusPreguntaToString(0)
   End Select
End Function

Public Function strStatusPreguntaToNum(ByVal statusPregunta As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusPregunta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSP_POR_RESPONDER To eSP_FALTA_INFORMACION
      If UCase(statusPregunta) = UCase(enumStatusPreguntaToString(mEnum)) Then
         strStatusPreguntaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusPreguntaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusPregunta & " no existe en tipo enumerativo statusPregunta"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusPregunta(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusPreguntaToString(enum_StatusPregunta.eSP_POR_RESPONDER)
      End If
      varComboBox.List(nCount) = enumStatusPreguntaToString(enum_StatusPregunta.eSP_POR_RESPONDER)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusPreguntaToString(enum_StatusPregunta.eSP_RESPONDIDA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusPreguntaToString(enum_StatusPregunta.eSP_FALTA_INFORMACION)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeComprobanteChequeAImprimirToString(ByVal tipoDeComprobanteChequeAImprimir As enum_TipoDeComprobanteChequeAImprimir) As String
   Select Case tipoDeComprobanteChequeAImprimir
      Case eKC_COMBANTECONCHEQUE: enumTipoDeComprobanteChequeAImprimirToString = "Comprobante de Impresión Con Cheque"
      Case eKC_SOLOORCOMPROBANTE: enumTipoDeComprobanteChequeAImprimirToString = "Solo Comprobante"
      Case eKC_COMPROBANTESEPARADODECHEQUE:  enumTipoDeComprobanteChequeAImprimirToString = "Comprobante Separado de Cheque"
      Case Else: enumTipoDeComprobanteChequeAImprimirToString = enumTipoDeComprobanteChequeAImprimirToString(0)
   End Select
End Function

Public Function enumTipoDeOrdenDePagoAImprimirToString(ByVal tipoDeOrdenDePagoAImprimir As enum_TipoDeOrdenDePagoAImprimir) As String
   Select Case tipoDeOrdenDePagoAImprimir
      Case eTD_ORDENDEPAGOCONCHEQUE: enumTipoDeOrdenDePagoAImprimirToString = "Orden De Pago Con Cheque"
      Case eTD_SOLOORDENDEPAGO: enumTipoDeOrdenDePagoAImprimirToString = "Solo Orden De Pago"
      Case eTD_ORDENDEPAGOSEPARADADECHEQUE: enumTipoDeOrdenDePagoAImprimirToString = "Orden De Pago Separada De Cheque"
      Case Else: enumTipoDeOrdenDePagoAImprimirToString = enumTipoDeOrdenDePagoAImprimirToString(0)
   End Select
End Function

Public Function strTipoComprovanteChequeAImprimirToNum(ByVal tipoDeComprobanteChequeAImprimir As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeComprobanteChequeAImprimir
   Dim nCount As Integer
   nCount = 0
   For mEnum = eKC_COMBANTECONCHEQUE To eKC_COMPROBANTESEPARADODECHEQUE
      If UCase(strTipoComprovanteChequeAImprimirToNum) = UCase(strTipoComprovanteChequeAImprimirToNum(mEnum)) Then
         strTipoComprovanteChequeAImprimirToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoComprovanteChequeAImprimirToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeComprobanteChequeAImprimir & " no existe en tipo enumerativo TipoDeComprobanteChequeAImprimir"
   End If
h_EXIT:
   Exit Function
End Function

Public Function tipoDeComprobanteChequeAImprimir(ByVal strTipoDeComprobanteChequeAImprimir As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeComprobanteChequeAImprimir
   Dim nCount As Integer
   nCount = 0
   For mEnum = eKC_COMBANTECONCHEQUE To eKC_COMPROBANTESEPARADODECHEQUE
      If UCase(strTipoDeComprobanteChequeAImprimir) = UCase(enumTipoDeComprobanteChequeAImprimirToString(mEnum)) Then
         strTipoDeComprobanteChequeAImprimir = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeComprobanteChequeAImprimir = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & strTipoDeComprobanteChequeAImprimir & " no existe en tipo enumerativo tipoDeOrdenDePagoAImprimir"
   End If
h_EXIT:
   Exit Function
End Function

Public Function strTipoDeOrdenDePagoAImprimirToNum(ByVal tipoDeOrdenDePagoAImprimir As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeOrdenDePagoAImprimir
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_ORDENDEPAGOCONCHEQUE To eTD_ORDENDEPAGOSEPARADADECHEQUE
      If UCase(tipoDeOrdenDePagoAImprimir) = UCase(enumTipoDeOrdenDePagoAImprimirToString(mEnum)) Then
         strTipoDeOrdenDePagoAImprimirToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeOrdenDePagoAImprimirToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeOrdenDePagoAImprimir & " no existe en tipo enumerativo tipoDeOrdenDePagoAImprimir"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeOrdenDePagoAImprimir(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeOrdenDePagoAImprimirToString(enum_TipoDeOrdenDePagoAImprimir.eTD_ORDENDEPAGOCONCHEQUE)
      End If
      varComboBox.List(nCount) = enumTipoDeOrdenDePagoAImprimirToString(enum_TipoDeOrdenDePagoAImprimir.eTD_ORDENDEPAGOCONCHEQUE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeOrdenDePagoAImprimirToString(enum_TipoDeOrdenDePagoAImprimir.eTD_SOLOORDENDEPAGO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeOrdenDePagoAImprimirToString(enum_TipoDeOrdenDePagoAImprimir.eTD_ORDENDEPAGOSEPARADADECHEQUE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusServicioToString(ByVal statusServicio As enum_StatusServicio) As String
   Select Case statusServicio
      Case eSS_PENDIENTE: enumStatusServicioToString = "Pendiente"
      Case eSS_EN_REVISION: enumStatusServicioToString = "En Revisión"
      Case eSS_NO_TIENE_SOLUCION: enumStatusServicioToString = "No Tiene Solución"
      Case eSS_ENVIADA_AL_CLIENTE: enumStatusServicioToString = "Enviada Al Cliente"
      Case eSS_REVISADA: enumStatusServicioToString = "Revisada"
      Case Else: enumStatusServicioToString = enumStatusServicioToString(0)
   End Select
End Function

Public Function strStatusServicioToNum(ByVal statusServicio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusServicio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSS_PENDIENTE To eSS_REVISADA
      If UCase(statusServicio) = UCase(enumStatusServicioToString(mEnum)) Then
         strStatusServicioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusServicioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusServicio & " no existe en tipo enumerativo statusServicio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusServicio(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusServicioToString(enum_StatusServicio.eSS_PENDIENTE)
      End If
      varComboBox.List(nCount) = enumStatusServicioToString(enum_StatusServicio.eSS_PENDIENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusServicioToString(enum_StatusServicio.eSS_EN_REVISION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusServicioToString(enum_StatusServicio.eSS_NO_TIENE_SOLUCION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusServicioToString(enum_StatusServicio.eSS_ENVIADA_AL_CLIENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusServicioToString(enum_StatusServicio.eSS_REVISADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumMontoPorcentajeToString(ByVal montoPorcentaje As enum_MontoPorcentaje) As String
   Select Case montoPorcentaje
      Case eMP_POR_MONTO: enumMontoPorcentajeToString = "Por Monto"
      Case eMP_POR_PORCENTAJE: enumMontoPorcentajeToString = "Por Porcentaje"
      Case Else: enumMontoPorcentajeToString = enumMontoPorcentajeToString(0)
   End Select
End Function

Public Function strMontoPorcentajeToNum(ByVal montoPorcentaje As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MontoPorcentaje
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMP_POR_MONTO To eMP_POR_PORCENTAJE
      If UCase(montoPorcentaje) = UCase(enumMontoPorcentajeToString(mEnum)) Then
         strMontoPorcentajeToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMontoPorcentajeToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & montoPorcentaje & " no existe en tipo enumerativo montoPorcentaje"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMontoPorcentaje(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumMontoPorcentajeToString(enum_MontoPorcentaje.eMP_POR_MONTO)
      End If
      varComboBox.List(nCount) = enumMontoPorcentajeToString(enum_MontoPorcentaje.eMP_POR_MONTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumMontoPorcentajeToString(enum_MontoPorcentaje.eMP_POR_PORCENTAJE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusCompraToString(ByVal statuscompra As enum_StatusCompra) As String
   Select Case statuscompra
      Case eSCp_VIGENTE: enumStatusCompraToString = "Vigente"
      Case eSCp_ANULADA: enumStatusCompraToString = "Anulada"
      Case Else: enumStatusCompraToString = enumStatusCompraToString(0)
   End Select
End Function

Public Function strStatusCompraToNum(ByVal statuscompra As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCompra
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSCp_VIGENTE To eSCp_ANULADA
      If UCase(statuscompra) = UCase(enumStatusCompraToString(mEnum)) Then
         strStatusCompraToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusCompraToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statuscompra & " no existe en tipo enumerativo statusCompra"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCompra(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusCompraToString(enum_StatusCompra.eSCp_VIGENTE)
      End If
      varComboBox.List(nCount) = enumStatusCompraToString(enum_StatusCompra.eSCp_VIGENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusCompraToString(enum_StatusCompra.eSCp_ANULADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusConteoFisicoToString(ByVal statusConteoFisico As enum_StatusConteoFisico) As String
   Select Case statusConteoFisico
      Case eSC_PROCESADO: enumStatusConteoFisicoToString = "Procesado"
      Case eSC_POR_PROCESAR: enumStatusConteoFisicoToString = "Por Procesar"
      Case eSC_ANULAR: enumStatusConteoFisicoToString = "Anulado"
      Case Else: enumStatusConteoFisicoToString = enumStatusConteoFisicoToString(0)
   End Select
End Function

Public Function strStatusConteoFisicoToNum(ByVal statusConteoFisico As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusConteoFisico
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_PROCESADO To eSC_ANULAR
      If UCase(statusConteoFisico) = UCase(enumStatusConteoFisicoToString(mEnum)) Then
         strStatusConteoFisicoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusConteoFisicoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusConteoFisico & " no existe en tipo enumerativo statusConteoFisico"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusConteoFisico(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusConteoFisicoToString(enum_StatusConteoFisico.eSC_PROCESADO)
      End If
      varComboBox.List(nCount) = enumStatusConteoFisicoToString(enum_StatusConteoFisico.eSC_PROCESADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusConteoFisicoToString(enum_StatusConteoFisico.eSC_POR_PROCESAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusConteoFisicoToString(enum_StatusConteoFisico.eSC_ANULAR)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumPermitirSobregiroToString(ByVal PermitirSobregiro As enum_PermitirSobregiro) As String
   Select Case PermitirSobregiro
      Case eCI_NO_CHEQUEAR_EXISTENCIA: enumPermitirSobregiroToString = "No Chequear Existencia"
      Case eCI_PERMITIR_SOBREGIRO: enumPermitirSobregiroToString = "Permitir Sobregiro"
      Case eCI_NO_PERMITIR_SOBREGIRO: enumPermitirSobregiroToString = "No Permitir Sobregiro"
      Case Else: enumPermitirSobregiroToString = enumPermitirSobregiroToString(0)
   End Select
End Function

Public Function strPermitirSobregiroToNum(ByVal PermitirSobregiro As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_PermitirSobregiro
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCI_NO_CHEQUEAR_EXISTENCIA To eCI_NO_PERMITIR_SOBREGIRO
      If UCase(PermitirSobregiro) = UCase(enumPermitirSobregiroToString(mEnum)) Then
         strPermitirSobregiroToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strPermitirSobregiroToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & PermitirSobregiro & " no existe en tipo enumerativo Chequeo De Inventario"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithPermitirSobregiro(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumPermitirSobregiroToString(enum_PermitirSobregiro.eCI_NO_CHEQUEAR_EXISTENCIA)
      End If
      varComboBox.List(nCount) = enumPermitirSobregiroToString(enum_PermitirSobregiro.eCI_NO_CHEQUEAR_EXISTENCIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPermitirSobregiroToString(enum_PermitirSobregiro.eCI_PERMITIR_SOBREGIRO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumPermitirSobregiroToString(enum_PermitirSobregiro.eCI_NO_PERMITIR_SOBREGIRO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString(ByVal tipoDeSolicitudDeIngresoDeTasaDeCambio As enum_TipoDeSolicitudDeIngresoDeTasaDeCambio) As String
   Select Case tipoDeSolicitudDeIngresoDeTasaDeCambio
      Case eTD_SIEMPREALEMITIRPRIMERAFACTURA: enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString = "Siempre al Emitir la primera Factura"
      Case eTD_SOLOSIESNECESARIO: enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString = "Sólo si es necesario"
      Case Else: enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString = enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString(0)
   End Select
End Function

Public Function strTipoDeSolicitudDeIngresoDeTasaDeCambioToNum(ByVal tipoDeSolicitudDeIngresoDeTasaDeCambio As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeSolicitudDeIngresoDeTasaDeCambio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_SIEMPREALEMITIRPRIMERAFACTURA To eTD_SOLOSIESNECESARIO
      If UCase(tipoDeSolicitudDeIngresoDeTasaDeCambio) = UCase(enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString(mEnum)) Then
         strTipoDeSolicitudDeIngresoDeTasaDeCambioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeSolicitudDeIngresoDeTasaDeCambioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeSolicitudDeIngresoDeTasaDeCambio & " no existe en tipo enumerativo tipoDeSolicitudDeIngresoDeTasaDeCambio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeSolicitudDeIngresoDeTasaDeCambio(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString(enum_TipoDeSolicitudDeIngresoDeTasaDeCambio.eTD_SIEMPREALEMITIRPRIMERAFACTURA)
      End If
      varComboBox.List(nCount) = enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString(enum_TipoDeSolicitudDeIngresoDeTasaDeCambio.eTD_SIEMPREALEMITIRPRIMERAFACTURA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeSolicitudDeIngresoDeTasaDeCambioToString(enum_TipoDeSolicitudDeIngresoDeTasaDeCambio.eTD_SOLOSIESNECESARIO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumCalculoParaComisionesSobreCobranzaEnBaseAToString(ByVal calculoParaComisionesSobreCobranzaEnBaseA As enum_CalculoParaComisionesSobreCobranzaEnBaseA) As String
   Select Case calculoParaComisionesSobreCobranzaEnBaseA
      Case eCP_MONTO: enumCalculoParaComisionesSobreCobranzaEnBaseAToString = "Monto"
      Case eCP_DIASVENCIDOS: enumCalculoParaComisionesSobreCobranzaEnBaseAToString = "Días Vencidos"
      Case eCP_PORCENTAJE_POR_ARTICULO: enumCalculoParaComisionesSobreCobranzaEnBaseAToString = "Porcentaje por Artículo"
      Case eCP_PORCENTAJE_POR_LINEA_DE_PRODUCTO: enumCalculoParaComisionesSobreCobranzaEnBaseAToString = "Porcentaje por Línea de Producto"
      Case eCP_MIXTO: enumCalculoParaComisionesSobreCobranzaEnBaseAToString = "Mixto"
      Case Else: enumCalculoParaComisionesSobreCobranzaEnBaseAToString = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(0)
   End Select
End Function

Public Function strComisionesSobreCobranzaToEnum(ByVal valComisionSobreCobranzasString As String) As enum_CalculoParaComisionesSobreCobranzaEnBaseA
   Select Case valComisionSobreCobranzasString
      Case enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO): strComisionesSobreCobranzaToEnum = eCP_MONTO
      Case enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_DIASVENCIDOS): strComisionesSobreCobranzaToEnum = eCP_DIASVENCIDOS
      Case enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO): strComisionesSobreCobranzaToEnum = eCP_PORCENTAJE_POR_ARTICULO
      Case enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_LINEA_DE_PRODUCTO): strComisionesSobreCobranzaToEnum = eCP_PORCENTAJE_POR_LINEA_DE_PRODUCTO
      Case enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MIXTO): strComisionesSobreCobranzaToEnum = eCP_MIXTO
      Case Else: strComisionesSobreCobranzaToEnum = eCP_MONTO
   End Select
End Function

Public Function strCalculoParaComisionesSobreCobranzaEnBaseAToNum(ByVal calculoParaComisionesSobreCobranzaEnBaseA As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_CalculoParaComisionesSobreCobranzaEnBaseA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCP_MONTO To eCP_MIXTO
      If UCase(calculoParaComisionesSobreCobranzaEnBaseA) = UCase(enumCalculoParaComisionesSobreCobranzaEnBaseAToString(mEnum)) Then
         strCalculoParaComisionesSobreCobranzaEnBaseAToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCalculoParaComisionesSobreCobranzaEnBaseAToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & calculoParaComisionesSobreCobranzaEnBaseA & " no existe en tipo enumerativo calculoParaComisionesSobreCobranzaEnBaseA"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCalculoParaComisionesSobreCobranzaEnBaseA(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir, _
                                                                           Optional mostrarDiasVencidos As Boolean = True)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_MONTO)
      End If
      varComboBox.List(nCount) = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_MONTO)
      nCount = nCount + 1
      If mostrarDiasVencidos Then
         varComboBox.List(nCount) = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_DIASVENCIDOS)
         nCount = nCount + 1
      End If
      varComboBox.List(nCount) = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_PORCENTAJE_POR_ARTICULO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_PORCENTAJE_POR_LINEA_DE_PRODUCTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumCalculoParaComisionesSobreCobranzaEnBaseAToString(enum_CalculoParaComisionesSobreCobranzaEnBaseA.eCP_MIXTO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumMotivoDelLoteToString(ByVal motivoDelLote As enum_MotivoDelLote) As String
   Select Case motivoDelLote
      Case eMD_ENTRADA_MANUAL: enumMotivoDelLoteToString = "Entrada Manual"
      Case eMD_EXPORTACION: enumMotivoDelLoteToString = "Exportacion"
      Case eMD_IMPORTACION: enumMotivoDelLoteToString = "Importacion"
      Case eMD_RESPALDO: enumMotivoDelLoteToString = "Respaldo"
      Case Else: enumMotivoDelLoteToString = enumMotivoDelLoteToString(0)
   End Select
End Function

Public Function strMotivoDelLoteToNum(ByVal motivoDelLote As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MotivoDelLote
   Select Case UCase(motivoDelLote)
      Case UCase(enumMotivoDelLoteToString(eMD_ENTRADA_MANUAL)): strMotivoDelLoteToNum = 0
      Case UCase(enumMotivoDelLoteToString(eMD_EXPORTACION)): strMotivoDelLoteToNum = 1
      Case UCase(enumMotivoDelLoteToString(eMD_IMPORTACION)): strMotivoDelLoteToNum = 2
      Case UCase(enumMotivoDelLoteToString(eMD_RESPALDO)): strMotivoDelLoteToNum = 3
      Case Else
         strMotivoDelLoteToNum = 0
         If showMessage Then
            gMessage.ProgrammerMessage "Valor " & motivoDelLote & " no existe en tipo enumerativo MotivoDelLote"
         End If
   End Select
h_EXIT:
   Exit Function
End Function


Public Sub FillComboBoxWithOpcionesParaElReporteListadoDePrecios(ByRef varComboBox As Object, ByVal valMonedaLocal As String, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAENMONEDALOCAL, valMonedaLocal)
      End If
      varComboBox.List(nCount) = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAENMONEDALOCAL, valMonedaLocal)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAMONEDAEXTRANJERA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAALATASADELDIAENMONEDALOCAL, valMonedaLocal)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox) - 50
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumOpcionesParaElReporteListadoDePreciosToString(ByVal ItemAImprimir As enum_OpcionesParaElReporteListadoDePrecios, Optional valMonedaLocal As String) As String
   Select Case ItemAImprimir
      Case eORLP_PRECIODEVENTAENMONEDALOCAL: enumOpcionesParaElReporteListadoDePreciosToString = "En " & valMonedaLocal
      Case eORLP_PRECIODEVENTAMONEDAEXTRANJERA: enumOpcionesParaElReporteListadoDePreciosToString = "En Moneda Extranjera"
      Case eORLP_PRECIODEVENTAALATASADELDIAENMONEDALOCAL: enumOpcionesParaElReporteListadoDePreciosToString = "En " & valMonedaLocal & " a la Tasa del Día"
      Case Else: enumOpcionesParaElReporteListadoDePreciosToString = enumOpcionesParaElReporteListadoDePreciosToString(0)
   End Select
End Function

Public Function enum_ambienteDeProgramaActualToStr(ByVal valAmbiente As Long) As String
   Select Case valAmbiente
      Case me_saw: enum_ambienteDeProgramaActualToStr = "Administrativo"
      Case me_WinCont: enum_ambienteDeProgramaActualToStr = "Contabilidad"
   End Select
End Function

Public Function enumTipoDeContabilizacionToString(ByVal tipoDeContabilizacion As enum_TipoDeContabilizacion) As String
   Select Case tipoDeContabilizacion
      Case eTD_CADA_DOCUMENTO: enumTipoDeContabilizacionToString = "Cada Documento"
      Case eTD_POR_LOTE: enumTipoDeContabilizacionToString = "Por Lote"
      Case Else: enumTipoDeContabilizacionToString = enumTipoDeContabilizacionToString(0)
   End Select
End Function

Public Function strTipoDeContabilizacionToNum(ByVal tipoDeContabilizacion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeContabilizacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_CADA_DOCUMENTO To eTD_POR_LOTE
      If UCase(tipoDeContabilizacion) = UCase(enumTipoDeContabilizacionToString(mEnum)) Then
         strTipoDeContabilizacionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeContabilizacionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeContabilizacion & " no existe en tipo enumerativo tipoDeContabilizacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeContabilizacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeContabilizacionToString(enum_TipoDeContabilizacion.eTD_CADA_DOCUMENTO)
      End If
      varComboBox.List(nCount) = enumTipoDeContabilizacionToString(enum_TipoDeContabilizacion.eTD_CADA_DOCUMENTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeContabilizacionToString(enum_TipoDeContabilizacion.eTD_POR_LOTE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumContabilizacionIndividualToString(ByVal contabilizacionIndividual As enum_ContabilizacionIndividual) As String
   Select Case contabilizacionIndividual
      Case eCI_INMEDIATA: enumContabilizacionIndividualToString = "Inmediata"
      Case eCI_POSPUESTA: enumContabilizacionIndividualToString = "Pospuesta"
      Case Else: enumContabilizacionIndividualToString = enumContabilizacionIndividualToString(0)
   End Select
End Function

Public Function strContabilizacionIndividualToNum(ByVal contabilizacionIndividual As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ContabilizacionIndividual
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCI_INMEDIATA To eCI_POSPUESTA
      If UCase(contabilizacionIndividual) = UCase(enumContabilizacionIndividualToString(mEnum)) Then
         strContabilizacionIndividualToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strContabilizacionIndividualToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & contabilizacionIndividual & " no existe en tipo enumerativo contabilizacionIndividual"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithContabilizacionIndividual(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumContabilizacionIndividualToString(enum_ContabilizacionIndividual.eCI_INMEDIATA)
      End If
      varComboBox.List(nCount) = enumContabilizacionIndividualToString(enum_ContabilizacionIndividual.eCI_INMEDIATA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumContabilizacionIndividualToString(enum_ContabilizacionIndividual.eCI_POSPUESTA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumContabilizacionPorLoteToString(ByVal contabilizacionPorLote As enum_ContabilizacionPorLote) As String
   Select Case contabilizacionPorLote
      Case eCP_DIARIA: enumContabilizacionPorLoteToString = "Diaria"
      Case eCP_MENSUAL: enumContabilizacionPorLoteToString = "Mensual"
      Case Else: enumContabilizacionPorLoteToString = enumContabilizacionPorLoteToString(0)
   End Select
End Function

Public Function strContabilizacionPorLoteToNum(ByVal contabilizacionPorLote As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ContabilizacionPorLote
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCP_DIARIA To eCP_MENSUAL
      If UCase(contabilizacionPorLote) = UCase(enumContabilizacionPorLoteToString(mEnum)) Then
         strContabilizacionPorLoteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strContabilizacionPorLoteToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & contabilizacionPorLote & " no existe en tipo enumerativo contabilizacionPorLote"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithContabilizacionPorLote(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumContabilizacionPorLoteToString(enum_ContabilizacionPorLote.eCP_DIARIA)
      End If
      varComboBox.List(nCount) = enumContabilizacionPorLoteToString(enum_ContabilizacionPorLote.eCP_DIARIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumContabilizacionPorLoteToString(enum_ContabilizacionPorLote.eCP_MENSUAL)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function strMenusDelSistemaToNum(ByVal menusDelSistema As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As menusDelSistemaAdministrativo
   Dim nCount As Integer
   nCount = 1
   For mEnum = me_PRINCIPALSAW To me_TABLASSAW_CONT
      If UCase(menusDelSistema) = UCase(enum_menusDelSistemaAdministrativoToStr(mEnum)) Then
         strMenusDelSistemaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMenusDelSistemaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & menusDelSistema & " no existe en tipo enumerativo menusDelSistema"
   End If
h_EXIT:
   Exit Function
End Function

Public Function strMenuTitleDelSistemaToNum(ByVal valMenuTitle As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As menusDelSistemaAdministrativo
   Dim nCount As Integer
   nCount = 1
   For mEnum = me_PRINCIPALSAW To me_TABLASSAW_CONT
      If UCase(valMenuTitle) = UCase(enum_menusDelSistemaAdministrativoToTitle(mEnum)) Then
         strMenuTitleDelSistemaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMenuTitleDelSistemaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valMenuTitle & " no existe en tipo enumerativo menusDelSistema"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMenusDelSistema(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_PRINCIPALSAW)
      End If
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_PRINCIPALSAW)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CXC)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_COMPANIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_TABLASSAW)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_INVENTARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CXP)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_BANCO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_PARAMETROS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_VEHICULOS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_IMPORTAREXPORTAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_PRINCIPALWINCONT)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_ACTIVOFIJO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_TABLASWINCONT)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_IMPORTEXPORTWINCONT)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CONTABILIZACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_COMPRAS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CONEXIONISLR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_PRINCIPALCAJA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CONEXIONAXI)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CAJA_CHICA_RENDICION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_ESTRUCTURA_COSTOS)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_CONVERSION_RESPALDO_LOTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_TABLASSAW_CONT)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_menusDelSistemaAdministrativoToStr(menusDelSistemaAdministrativo.me_PRODUCCION)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enum_menusDelSistemaAdministrativoToTitle(ByVal valMenu As menusDelSistemaAdministrativo) As String
   Select Case valMenu
      Case me_PRINCIPALSAW: enum_menusDelSistemaAdministrativoToTitle = "Menú Principal - Factura / Cotización / Contrato"
      Case me_CXC
            If GetEsSistemaAdmInterno Then
               enum_menusDelSistemaAdministrativoToTitle = "CxC / Cobranzas / Vendedor / Orden de Servicio"
            Else
               enum_menusDelSistemaAdministrativoToTitle = "CxC / Cobranzas / Vendedor"
            End If
      Case me_COMPANIA: enum_menusDelSistemaAdministrativoToTitle = "Empresa"
      Case me_TABLASSAW: enum_menusDelSistemaAdministrativoToTitle = "Mantenimiento de Tablas"
      Case me_INVENTARIO
            If GetEsSistemaAdmInterno Then
               enum_menusDelSistemaAdministrativoToTitle = "Inventario / Revisión de Data"
            Else
               enum_menusDelSistemaAdministrativoToTitle = "Inventario"
            End If
      Case me_CXP
            If gGlobalization.fEsCodigoVenezuela Then
               enum_menusDelSistemaAdministrativoToTitle = "CxP / Pagos / Proveedores / Forma 30(IVA)"
            ElseIf gGlobalization.fEsCodigoPeru Then
               enum_menusDelSistemaAdministrativoToTitle = "CxP / Pagos / Proveedores"
            End If
      Case me_BANCO: enum_menusDelSistemaAdministrativoToTitle = "Bancos"
      
      Case me_PARAMETROS
            If gGlobalization.fEsCodigoVenezuela Then
               enum_menusDelSistemaAdministrativoToTitle = "Usuarios / Parámetros"
            Else
               enum_menusDelSistemaAdministrativoToTitle = "Usuarios / Parámetros"
            End If
      Case me_VEHICULOS: enum_menusDelSistemaAdministrativoToTitle = "Vehículos"
      Case me_IMPORTAREXPORTAR: enum_menusDelSistemaAdministrativoToTitle = "Importar / Exportar"
      Case me_PRINCIPALWINCONT: enum_menusDelSistemaAdministrativoToTitle = "Comprobantes / Cuentas / Balances / Periodo"
      Case me_ACTIVOFIJO: enum_menusDelSistemaAdministrativoToTitle = "Activos Fijos"
      Case me_TABLASWINCONT: enum_menusDelSistemaAdministrativoToTitle = "Auxiliares / Centro De Costos / Tipo de Comprobante / Otros"
      Case me_IMPORTEXPORTWINCONT: enum_menusDelSistemaAdministrativoToTitle = "Importar-Exportar Datos"
      Case me_CONTABILIZACION: enum_menusDelSistemaAdministrativoToTitle = "Contabilizar / Reglas de Contabilización"
      Case me_PRIVATE: enum_menusDelSistemaAdministrativoToTitle = "Solo Programadores"
      Case me_COMPRAS: enum_menusDelSistemaAdministrativoToTitle = "Compras"
      Case me_INFORME_DE_AUDITORIA: enum_menusDelSistemaAdministrativoToTitle = "Informes de Auditoria"
      Case me_CONEXIONISLR: enum_menusDelSistemaAdministrativoToTitle = "Impuesto sobre la Renta"
      Case me_PRINCIPALCAJA
         If GetEsSistemaAdmInterno Then
               enum_menusDelSistemaAdministrativoToTitle = ""
         Else
               enum_menusDelSistemaAdministrativoToTitle = "Factura / Cliente / Inventario / Caja"
         End If
      Case me_CONEXIONAXI: enum_menusDelSistemaAdministrativoToTitle = "Ajuste por Inflación"
      Case me_CAJA_CHICA_RENDICION: enum_menusDelSistemaAdministrativoToTitle = "Reposición de Caja Chica"
      Case me_ESTRUCTURA_COSTOS: enum_menusDelSistemaAdministrativoToTitle = "Estructura de Costos"
      Case me_CONVERSION_RESPALDO_LOTE: enum_menusDelSistemaAdministrativoToTitle = "Conversión / Respaldar / Lote"
      Case me_TABLASSAW_CONT: enum_menusDelSistemaAdministrativoToTitle = "Mantenimiento de Tablas Continuación"
      Case me_PRODUCCION: enum_menusDelSistemaAdministrativoToTitle = "Producción"
      Case Else: enum_menusDelSistemaAdministrativoToTitle = enum_menusDelSistemaAdministrativoToTitle(0)
   End Select
End Function

Public Function fenumBaseFormulaOtrosCargosDeFacturaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_BaseFormulaOtrosCargosDeFactura
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_BaseFormulaOtrosCargosDeFactura.eBF_TOTALFACTURA - enum_BaseFormulaOtrosCargosDeFactura.eBF_SUBTOTAL
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eBF_SUBTOTAL To eBF_TOTALFACTURA
      varArray(nCount) = varCaracterDelimitador & enumBaseFormulaOtrosCargosDeFacturaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumBaseFormulaOtrosCargosDeFacturaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fenumTipoDocumentoFacturaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoFactura
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoFactura.eTF_NOTAENTREGA - enum_TipoDocumentoFactura.eTF_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTF_FACTURA To eTF_NOTAENTREGA
      If valConSiglas = False Then
      varArray(nCount) = varCaracterDelimitador & enumTipodocumentoFacturaToString(varEnum) & varCaracterDelimitador
      Else
      varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoFacturaToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fenumTipoDocumentoFacturaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDocumentoFacturaToSiglas(tipoDocumento As enum_TipoDocumentoFactura) As String
   Select Case tipoDocumento
      Case eTF_FACTURA: enumTipoDocumentoFacturaToSiglas = "FAC"
      Case eTF_NOTADECREDITO: enumTipoDocumentoFacturaToSiglas = "N/C"
      Case eTF_NOTADEDEBITO: enumTipoDocumentoFacturaToSiglas = "N/D"
      Case eTF_RESUMENDIARIODEVENTAS: enumTipoDocumentoFacturaToSiglas = "R/D"
      Case eTF_NOASIGNADO: enumTipoDocumentoFacturaToSiglas = "N/A"
      Case eTF_COMPROBANTEFISCAL: enumTipoDocumentoFacturaToSiglas = "CF"
      Case eTF_BOLETA: enumTipoDocumentoFacturaToSiglas = "BL"
      Case eTF_NOTADECREDITOCOMPROBANTEFISCAL: enumTipoDocumentoFacturaToSiglas = "N/C-CF"
      Case eTF_NOTAENTREGA: enumTipoDocumentoFacturaToSiglas = "NE"
      Case Else: enumTipoDocumentoFacturaToSiglas = enumTipoDocumentoFacturaToSiglas(0)
   End Select
End Function

Public Function fenumTipoDocumentoCxPToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoCxP
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoCxP.eTCxP_NOTADEENTREGA - enum_TipoDocumentoCxP.eTCxP_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTCxP_FACTURA To eTCxP_NOTADEENTREGA
      varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoCxPToSiglas(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTipoDocumentoCxPToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDocumentoCxPToSiglas(tipoDocumento As enum_TipoDocumentoCxP) As String
   Select Case tipoDocumento
      Case eTCxP_FACTURA: enumTipoDocumentoCxPToSiglas = "FAC"
      Case eTCxP_GIRO: enumTipoDocumentoCxPToSiglas = "FAC"
      Case eTCxP_CHEQUEDEVUELTO: enumTipoDocumentoCxPToSiglas = "N/D"
      Case eTCxP_NOTADECREDITO: enumTipoDocumentoCxPToSiglas = "N/C"
      Case eTCxP_NOTADEDEBITO: enumTipoDocumentoCxPToSiglas = "N/D"
      Case eTCxP_NOTADEENTREGA: enumTipoDocumentoCxPToSiglas = "N/E"
      Case Else: enumTipoDocumentoCxPToSiglas = enumTipoDocumentoCxPToSiglas(0)
   End Select
End Function

Public Function fenumTipoCompraToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeCompra
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeCompra.eTD_COMPRASSINDERECHOACREDITO - enum_TipoDeCompra.eTD_COMPRASEXENTAS
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTD_COMPRASEXENTAS To eTD_COMPRASSINDERECHOACREDITO
      varArray(nCount) = varCaracterDelimitador & enumTipoDeCompraToStringSiglas(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTipoCompraToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fenumComoAplicaOtrosCargosDeFacturaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_ComoAplicaOtrosCargosDeFactura
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO - enum_ComoAplicaOtrosCargosDeFactura.eCA_SUMA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eCA_SUMA To eCA_NO_APLICA_SOLO_INFORMATIVO
      varArray(nCount) = varCaracterDelimitador & enumComoAplicaOtrosCargosDeFacturaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumComoAplicaOtrosCargosDeFacturaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumStatusVendedorToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_StatusVendedor
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusVendedor.eSV_RESTRINGIDO - enum_StatusVendedor.eSV_ACTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSV_ACTIVO To eSV_RESTRINGIDO
      If valConSiglas = False Then
      varArray(nCount) = varCaracterDelimitador & enumStatusVendedorToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumStatusVendedorToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumStatusVendedorToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumTipoDeArticuloToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeArticulo
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeArticulo.eTD_PRODUCTO_COMPUESTO - enum_TipoDeArticulo.eTD_MERCANCIA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTD_MERCANCIA To eTD_PRODUCTO_COMPUESTO
      varArray(nCount) = varCaracterDelimitador & enumTipoDeArticuloToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeArticuloToStringInArray = varArray
   On Error GoTo 0
End Function
Public Function fenumStatusCursoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusCurso
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusCurso.eSC_DICTADO - enum_StatusCurso.eSC_DISPONIBLE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSC_DISPONIBLE To eSC_DICTADO
      varArray(nCount) = varCaracterDelimitador & enumStatusCursoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumStatusCursoToStringInArray = varArray
   On Error GoTo 0
End Function
Public Function fEnumGeneradoPorToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_GeneradoPor
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_GeneradoPor.eGP_RETENCION_EN_COBRANZA - enum_GeneradoPor.eGP_USUARIO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eGP_USUARIO To eGP_RETENCION_EN_COBRANZA
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumGeneradoPorToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumGeneradoPorToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   varEnum = eGP_ANTICIPO
   varArray(nCount) = varCaracterDelimitador & enumGeneradoPorToString(varEnum) & varCaracterDelimitador
   
   fEnumGeneradoPorToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumTipoDeContribuyenteToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeContribuyente
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeContribuyente.eTD_NOCONTRIBUYENTE - enum_TipoDeContribuyente.eTD_CONTRIBUYENTE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTD_CONTRIBUYENTE To eTD_NOCONTRIBUYENTE
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTipoDeContribuyenteToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTipoDeContribuyenteToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumTipoDeContribuyenteToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumStatusConciliacionToString(ByVal statusConciliacion As enum_StatusConciliacion) As String
   Select Case statusConciliacion
      Case eSC_ENPROCESO: enumStatusConciliacionToString = "EN PROCESO"
      Case eSC_CERRADA: enumStatusConciliacionToString = "CERRADA"
      Case Else: enumStatusConciliacionToString = enumStatusConciliacionToString(0)
   End Select
End Function

Public Function strStatusConciliacionToNum(ByVal statusConciliacion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusConciliacion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_ENPROCESO To eSC_CERRADA
      If UCase(statusConciliacion) = UCase(enumStatusConciliacionToString(mEnum)) Then
         strStatusConciliacionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusConciliacionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusConciliacion & " no existe en tipo enumerativo statusConciliacion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusConciliacion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusConciliacionToString(enum_StatusConciliacion.eSC_ENPROCESO)
      End If
      varComboBox.List(nCount) = enumStatusConciliacionToString(enum_StatusConciliacion.eSC_ENPROCESO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusConciliacionToString(enum_StatusConciliacion.eSC_CERRADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumCategoriaLlamadaToString(ByVal categoriaLlamada As enum_CategoriaLlamada) As String
   Select Case categoriaLlamada
      Case eCL_POR_DEFINIR: enumCategoriaLlamadaToString = "Por Definir"
      Case eCL_ENTRANTE: enumCategoriaLlamadaToString = "Entrante"
      Case eCL_SALIENTE: enumCategoriaLlamadaToString = "Saliente"
      Case eCL_ATENCION_EN_OFICINA: enumCategoriaLlamadaToString = "Atención en Oficina"
      Case eCL_FAX: enumCategoriaLlamadaToString = "FAX"
      Case Else: enumCategoriaLlamadaToString = enumCategoriaLlamadaToString(0)
   End Select
End Function

Public Function strCategoriaLlamadaToNum(ByVal categoriaLlamada As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_CategoriaLlamada
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCL_POR_DEFINIR To eCL_FAX
      If UCase(categoriaLlamada) = UCase(enumCategoriaLlamadaToString(mEnum)) Then
         strCategoriaLlamadaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCategoriaLlamadaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & categoriaLlamada & " no existe en tipo enumerativo categoriaLlamada"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCategoriaLlamada(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCategoriaLlamadaToString(enum_CategoriaLlamada.eCL_POR_DEFINIR)
   End If
   varComboBox.List(nCount) = enumCategoriaLlamadaToString(enum_CategoriaLlamada.eCL_POR_DEFINIR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCategoriaLlamadaToString(enum_CategoriaLlamada.eCL_ENTRANTE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCategoriaLlamadaToString(enum_CategoriaLlamada.eCL_SALIENTE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCategoriaLlamadaToString(enum_CategoriaLlamada.eCL_ATENCION_EN_OFICINA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCategoriaLlamadaToString(enum_CategoriaLlamada.eCL_FAX)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumComoSeEnteroToString(ByVal comoSeEntero As enum_ComoSeEntero) As String
   Select Case comoSeEntero
      Case eCS_POR_DEFINIR: enumComoSeEnteroToString = "Por Definir"
      Case eCS_EL_UNIVERSAL: enumComoSeEnteroToString = "El Universal"
      Case eCS_EL_NACIONAL: enumComoSeEnteroToString = "El Nacional"
      Case eCS_ULTIMAS_NOTICIAS: enumComoSeEnteroToString = "Ultimas Noticias"
      Case eCS_REFERIDO: enumComoSeEnteroToString = "Referido"
      Case eCS_CAVEGUIAS: enumComoSeEnteroToString = "Caveguias"
      Case eCS_INTERNET: enumComoSeEnteroToString = "Internet"
      Case eCS_YA_ES_CLIENTE: enumComoSeEnteroToString = "Ya Es Cliente"
      Case eCS_EVENTOS: enumComoSeEnteroToString = "Eventos"
      Case eCS_OTRO: enumComoSeEnteroToString = "Otro"
      Case Else: enumComoSeEnteroToString = enumComoSeEnteroToString(0)
   End Select
End Function

Public Function strComoSeEnteroToNum(ByVal comoSeEntero As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ComoSeEntero
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCS_POR_DEFINIR To eCS_OTRO
      If UCase(comoSeEntero) = UCase(enumComoSeEnteroToString(mEnum)) Then
         strComoSeEnteroToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strComoSeEnteroToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & comoSeEntero & " no existe en tipo enumerativo comoSeEntero"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithComoSeEntero(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_POR_DEFINIR)
   End If
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_POR_DEFINIR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_EL_UNIVERSAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_EL_NACIONAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_ULTIMAS_NOTICIAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_REFERIDO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_CAVEGUIAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_INTERNET)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_YA_ES_CLIENTE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_EVENTOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComoSeEnteroToString(enum_ComoSeEntero.eCS_OTRO)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumFormaDeLaInicialToString(ByVal formaDeLaInicial As enum_FormaDeLaInicial) As String
   Select Case formaDeLaInicial
      Case eFD_POR_MONTO: enumFormaDeLaInicialToString = "Por Monto"
      Case eFD_POR_PORCENTAJE: enumFormaDeLaInicialToString = "Por Porcentaje"
      Case Else: enumFormaDeLaInicialToString = enumFormaDeLaInicialToString(0)
   End Select
End Function

Public Function enumTipodeMovimientosToString(ByVal TipodeMovimientos As enum_TipodeMovimientos) As String
   Select Case TipodeMovimientos
      Case eTM_Conciliados: enumTipodeMovimientosToString = "Conciliados"
      Case eTM_NoConciliados: enumTipodeMovimientosToString = "No Conciliados"
      Case eTM_Todos: enumTipodeMovimientosToString = "Todos"
      Case Else: enumTipodeMovimientosToString = enumTipodeMovimientosToString(0)
   End Select
End Function

Public Sub FillComboBoxWithTipodeMovimientos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipodeMovimientosToString(enum_TipodeMovimientos.eTM_Conciliados)
      End If
      varComboBox.List(nCount) = enumTipodeMovimientosToString(enum_TipodeMovimientos.eTM_Conciliados)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodeMovimientosToString(enum_TipodeMovimientos.eTM_NoConciliados)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipodeMovimientosToString(enum_TipodeMovimientos.eTM_Todos)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumStatusDeCotizacionGalacToString(ByVal statusDeCotizacionGalac As enum_StatusDeCotizacionGalac) As String
   Select Case statusDeCotizacionGalac
      Case eSD_PORCONFIRMAR: enumStatusDeCotizacionGalacToString = "Por Confirmar"
      Case eSD_VOLVERALLAMAR: enumStatusDeCotizacionGalacToString = "Volver a Llamar"
      Case eSD_NOCONTESTA: enumStatusDeCotizacionGalacToString = "No Contesta"
      Case eSD_REENVIADA: enumStatusDeCotizacionGalacToString = "Reenviada"
      Case eSD_FACTURADA: enumStatusDeCotizacionGalacToString = "Facturada"
      Case eSD_RECHAZADA: enumStatusDeCotizacionGalacToString = "Rechazada"
      Case eSD_NODEFINIDO: enumStatusDeCotizacionGalacToString = "No Definido"
      Case eSD_CONFIRMADA: enumStatusDeCotizacionGalacToString = "Confirmada"
      Case eSD_FACTURADA_EN_OTRA_COTIZACION: enumStatusDeCotizacionGalacToString = "Fact.Otra Cotización"
      Case Else: enumStatusDeCotizacionGalacToString = enumStatusDeCotizacionGalacToString(0)
   End Select
End Function

Public Function strStatusDeCotizacionGalacToNum(ByVal statusDeCotizacionGalac As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusDeCotizacionGalac
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSD_PORCONFIRMAR To eSD_FACTURADA_EN_OTRA_COTIZACION
      If UCase(statusDeCotizacionGalac) = UCase(enumStatusDeCotizacionGalacToString(mEnum)) Then
         strStatusDeCotizacionGalacToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusDeCotizacionGalacToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusDeCotizacionGalac & " no existe en tipo enumerativo statusDeCotizacionGalac"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusDeCotizacionGalac(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_PORCONFIRMAR)
      End If
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_PORCONFIRMAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_VOLVERALLAMAR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_NOCONTESTA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_REENVIADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_FACTURADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_RECHAZADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_NODEFINIDO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_CONFIRMADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDeCotizacionGalacToString(enum_StatusDeCotizacionGalac.eSD_FACTURADA_EN_OTRA_COTIZACION)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumModoDeEnvioToString(ByVal modoDeEnvio As enum_ModoDeEnvio) As String
   Select Case modoDeEnvio
      Case eMD_EMAIL: enumModoDeEnvioToString = "E-Mail"
      Case eMD_FAX: enumModoDeEnvioToString = "Fax"
      Case eMD_IMPRESO: enumModoDeEnvioToString = "impreso"
      Case Else: enumModoDeEnvioToString = enumModoDeEnvioToString(0)
   End Select
End Function

Public Function strModoDeEnvioToNum(ByVal modoDeEnvio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ModoDeEnvio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMD_EMAIL To eMD_IMPRESO
      If UCase(modoDeEnvio) = UCase(enumModoDeEnvioToString(mEnum)) Then
         strModoDeEnvioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strModoDeEnvioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & modoDeEnvio & " no existe en tipo enumerativo modoDeEnvio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithModoDeEnvio(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumModoDeEnvioToString(enum_ModoDeEnvio.eMD_EMAIL)
      End If
      varComboBox.List(nCount) = enumModoDeEnvioToString(enum_ModoDeEnvio.eMD_EMAIL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumModoDeEnvioToString(enum_ModoDeEnvio.eMD_FAX)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumModoDeEnvioToString(enum_ModoDeEnvio.eMD_IMPRESO)
      varComboBox.Width = 2265 ' gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumInfoGalacModoEnvioToString(ByVal infoGalacModoEnvio As enum_InfoGalacModoEnvio) As String
   Select Case infoGalacModoEnvio
      Case eIG_POR_DEFINIR: enumInfoGalacModoEnvioToString = "Por Definir"
      Case eIG_FAX: enumInfoGalacModoEnvioToString = "Fax"
      Case eIG_EMAIL: enumInfoGalacModoEnvioToString = "E-Mail (web)"
      Case eIG_MAIL_Y_FAX: enumInfoGalacModoEnvioToString = "Fax y Mail (web)"
      Case eIG_NO_POR_AHORA: enumInfoGalacModoEnvioToString = "No por Ahora"
      Case eIG_NO_TIENE_FAXMAIL: enumInfoGalacModoEnvioToString = "No tiene Fax-Mail"
      Case eIG_YA_SUSCRITO: enumInfoGalacModoEnvioToString = "Ya Suscrito"
      Case Else: enumInfoGalacModoEnvioToString = enumInfoGalacModoEnvioToString(0)
   End Select
End Function

Public Function strInfoGalacModoEnvioToNum(ByVal infoGalacModoEnvio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_InfoGalacModoEnvio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eIG_POR_DEFINIR To eIG_YA_SUSCRITO
      If UCase(infoGalacModoEnvio) = UCase(enumInfoGalacModoEnvioToString(mEnum)) Then
         strInfoGalacModoEnvioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strInfoGalacModoEnvioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & infoGalacModoEnvio & " no existe en tipo enumerativo infoGalacModoEnvio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithInfoGalacModoEnvio(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_POR_DEFINIR)
      End If
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_POR_DEFINIR)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_FAX)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_EMAIL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_MAIL_Y_FAX)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_NO_POR_AHORA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_NO_TIENE_FAXMAIL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumInfoGalacModoEnvioToString(enum_InfoGalacModoEnvio.eIG_YA_SUSCRITO)
      varComboBox.Width = 1950
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTalonarioToString(ByVal talonario As enum_Talonario) As String
   Select Case talonario
      Case eTa_TALONARIO1: enumTalonarioToString = "Talonario No. 1"
      Case eTa_TALONARIO2: enumTalonarioToString = "Talonario No. 2"
      Case Else: enumTalonarioToString = enumTalonarioToString(0)
   End Select
End Function

Public Function strTalonarioToNum(ByVal talonario As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Talonario
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTa_TALONARIO1 To eTa_TALONARIO2
      If UCase(talonario) = UCase(enumTalonarioToString(mEnum)) Then
         strTalonarioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTalonarioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & talonario & " no existe en tipo enumerativo talonario"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTalonario(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTalonarioToString(enum_Talonario.eTa_TALONARIO1)
      End If
      varComboBox.List(nCount) = enumTalonarioToString(enum_Talonario.eTa_TALONARIO1)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTalonarioToString(enum_Talonario.eTa_TALONARIO2)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumEnviadoViaToString(ByVal enviadoVia As enum_EnviadoVia) As String
   Select Case enviadoVia
      Case eEV_NO_ENVIADO: enumEnviadoViaToString = "No Enviado"
      Case eEV_FAX: enumEnviadoViaToString = "Fax"
      Case eEV_EMAIL: enumEnviadoViaToString = "E-Mail"
      Case Else: enumEnviadoViaToString = enumEnviadoViaToString(0)
   End Select
End Function

Public Function strEnviadoViaToNum(ByVal enviadoVia As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_EnviadoVia
   Dim nCount As Integer
   nCount = 0
   For mEnum = eEV_NO_ENVIADO To eEV_EMAIL
      If UCase(enviadoVia) = UCase(enumEnviadoViaToString(mEnum)) Then
         strEnviadoViaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strEnviadoViaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & enviadoVia & " no existe en tipo enumerativo enviadoVia"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithEnviadoVia(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumEnviadoViaToString(enum_EnviadoVia.eEV_NO_ENVIADO)
      End If
      varComboBox.List(nCount) = enumEnviadoViaToString(enum_EnviadoVia.eEV_NO_ENVIADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumEnviadoViaToString(enum_EnviadoVia.eEV_FAX)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumEnviadoViaToString(enum_EnviadoVia.eEV_EMAIL)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function strTipoDocumentoEnviadoToNum(ByVal tipoDocumentoEnviado As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDocumentoEnviado
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_VENTA To eTD_COBRANZA
      If UCase(tipoDocumentoEnviado) = UCase(enumTipoDocumentoEnviadoToString(mEnum)) Then
         strTipoDocumentoEnviadoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDocumentoEnviadoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDocumentoEnviado & " no existe en tipo enumerativo tipoDocumentoEnviado"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumStatusCxCToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_StatusDocumento
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusDocumento.eSD_REFINANCIADO - enum_StatusDocumento.eSD_PORCANCELAR
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusDocumento.eSD_PORCANCELAR To enum_StatusDocumento.eSD_REFINANCIADO
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumStatusDocumentoToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumStatusDocumentoToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumStatusCxCToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumStatusClienteToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusCliente
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusCliente.eSC_DATOS_DESACTUALIZADOS - enum_StatusCliente.eSC_ACTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusCliente.eSC_ACTIVO To enum_StatusCliente.eSC_DATOS_DESACTUALIZADOS
      varArray(nCount) = varCaracterDelimitador & enumStatusClienteToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusClienteToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDocumentoEnviadoToString(ByVal tipoDocumentoEnviado As enum_TipoDocumentoEnviado) As String
   Select Case tipoDocumentoEnviado
      Case eTD_VENTA: enumTipoDocumentoEnviadoToString = "Venta"
      Case eTD_SERVICIO_DE_ENVIO: enumTipoDocumentoEnviadoToString = "Servicio"
      Case eTD_CLAVE: enumTipoDocumentoEnviadoToString = "Clave"
      Case eTD_COBRANZA: enumTipoDocumentoEnviadoToString = "Cobranza"
      Case Else: enumTipoDocumentoEnviadoToString = enumTipoDocumentoEnviadoToString(0)
   End Select
End Function

Public Function fEnumTipoDeEnvioToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_EnviadoVia
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_EnviadoVia.eEV_EMAIL - enum_EnviadoVia.eEV_NO_ENVIADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_EnviadoVia.eEV_NO_ENVIADO To enum_EnviadoVia.eEV_EMAIL
      varArray(nCount) = varCaracterDelimitador & enumEnviadoViaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeEnvioToStringInArray = varArray
   On Error GoTo 0
End Function

Public Sub FillComboBoxWithTipoDocumentoEnviado(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDocumentoEnviadoToString(enum_TipoDocumentoEnviado.eTD_VENTA)
      End If
      varComboBox.List(nCount) = enumTipoDocumentoEnviadoToString(enum_TipoDocumentoEnviado.eTD_VENTA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoEnviadoToString(enum_TipoDocumentoEnviado.eTD_SERVICIO_DE_ENVIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoEnviadoToString(enum_TipoDocumentoEnviado.eTD_CLAVE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoEnviadoToString(enum_TipoDocumentoEnviado.eTD_COBRANZA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumTipoDeDocumentoEnviadoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoEnviado
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoEnviado.eTD_COBRANZA - enum_TipoDocumentoEnviado.eTD_VENTA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDocumentoEnviado.eTD_VENTA To enum_TipoDocumentoEnviado.eTD_COBRANZA
'     If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoEnviadoToString(varEnum) & varCaracterDelimitador
'     Else
        ' varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoEnviadoToSiglas(varEnum) & varCaracterDelimitador
'     End If
         nCount = nCount + 1
   Next
   fEnumTipoDeDocumentoEnviadoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enum_VerPromocionNoPromocionTodosToString(ByVal promocion As enum_VerPromocionNoPromocionTodos) As String
   Select Case promocion
      Case eVer_TodosPromocion: enum_VerPromocionNoPromocionTodosToString = "Todos"
      Case eVer_Promocion: enum_VerPromocionNoPromocionTodosToString = "Promoción"
      Case eVer_NoPromocion: enum_VerPromocionNoPromocionTodosToString = "No Promoción"
   End Select
End Function

Public Sub FillComboBoxWithVerPromocionNoPromocionTodos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enum_VerPromocionNoPromocionTodosToString(enum_VerPromocionNoPromocionTodos.eVer_TodosPromocion)
      End If
      varComboBox.List(nCount) = enum_VerPromocionNoPromocionTodosToString(enum_VerPromocionNoPromocionTodos.eVer_TodosPromocion)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_VerPromocionNoPromocionTodosToString(enum_VerPromocionNoPromocionTodos.eVer_Promocion)
      nCount = nCount + 1
      varComboBox.List(nCount) = enum_VerPromocionNoPromocionTodosToString(enum_VerPromocionNoPromocionTodos.eVer_NoPromocion)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enum_EnviadorODestinatarioToString(ByVal enviadordestinatario As enum_EnviadorODestinatario) As String
   Select Case enviadordestinatario
      Case eEM_ENVIADOR: enum_EnviadorODestinatarioToString = "Enviador"
      Case eEM_DESTINATARIO: enum_EnviadorODestinatarioToString = "Destinatario"
   End Select
End Function

Public Function enum_NivelesVentayCobranzaToString(ByVal nivelVentaoCobranza As enum_NivelesVentayCobranza) As String
   Select Case nivelVentaoCobranza
      Case eNV_NIVEL1: enum_NivelesVentayCobranzaToString = "Nivel 1"
      Case eNV_NIVEL2: enum_NivelesVentayCobranzaToString = "Nivel 2"
      Case eNV_NIVEL3: enum_NivelesVentayCobranzaToString = "Nivel 3"
      Case eNV_NIVEL4: enum_NivelesVentayCobranzaToString = "Nivel 4"
      Case eNV_NIVEL5: enum_NivelesVentayCobranzaToString = "Nivel 5"
   End Select
End Function

Public Function enumGenerarMovBancarioDesdeCobroToString(ByVal GenerarMovBancarioDesdeCobro As enum_GenerarMovBancarioDesdeCobro) As String
   Select Case GenerarMovBancarioDesdeCobro
      Case eGM_Siempre: enumGenerarMovBancarioDesdeCobroToString = "Siempre"
      Case eGM_Preguntar: enumGenerarMovBancarioDesdeCobroToString = "Preguntar"
      Case eGM_Nunca: enumGenerarMovBancarioDesdeCobroToString = "Nunca"
      Case Else: enumGenerarMovBancarioDesdeCobroToString = enumGenerarMovBancarioDesdeCobroToString(0)
   End Select
End Function

Public Function strGenerarMovBancarioDesdeCobroToNum(ByVal GenerarMovBancarioDesdeCobro As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_GenerarMovBancarioDesdeCobro
   Dim nCount As Integer
   nCount = 0
   For mEnum = eGM_Siempre To eGM_Nunca
   If UCase(GenerarMovBancarioDesdeCobro) = UCase(enumGenerarMovBancarioDesdeCobroToString(mEnum)) Then
      strGenerarMovBancarioDesdeCobroToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strGenerarMovBancarioDesdeCobroToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & GenerarMovBancarioDesdeCobro & " no existe en tipo enumerativo GenerarMovBancarioDesdeCobro"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithGenerarMovBancarioDesdeCobro(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumGenerarMovBancarioDesdeCobroToString(enum_GenerarMovBancarioDesdeCobro.eGM_Siempre)
      End If
      varComboBox.List(nCount) = enumGenerarMovBancarioDesdeCobroToString(enum_GenerarMovBancarioDesdeCobro.eGM_Siempre)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGenerarMovBancarioDesdeCobroToString(enum_GenerarMovBancarioDesdeCobro.eGM_Preguntar)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumGenerarMovBancarioDesdeCobroToString(enum_GenerarMovBancarioDesdeCobro.eGM_Nunca)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumTipoDeServicioToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeServicio
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeServicio.eTD_OTRO - enum_TipoDeServicio.eTD_ACTUALIZACION
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeServicio.eTD_ACTUALIZACION To enum_TipoDeServicio.eTD_OTRO
      varArray(nCount) = varCaracterDelimitador & enumTipoDeServicioToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeServicioToStringInArray = varArray
   On Error GoTo 0
End Function

Public Sub FillComboBoxWithOpcionesParaElReporteListadoDePreciosVersion2(ByRef varComboBox As Object, ByVal valMonedaLocal As String)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      varComboBox.Text = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAENMONEDALOCAL, valMonedaLocal)
      varComboBox.List(nCount) = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAENMONEDALOCAL, valMonedaLocal)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumOpcionesParaElReporteListadoDePreciosToString(enum_OpcionesParaElReporteListadoDePrecios.eORLP_PRECIODEVENTAMONEDAEXTRANJERA)
      varComboBox.ListIndex = 0
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function fEnumStatusConteoFisicoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusConteoFisico
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusConteoFisico.eSC_ANULAR - enum_StatusConteoFisico.eSC_PROCESADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusConteoFisico.eSC_PROCESADO To enum_StatusConteoFisico.eSC_ANULAR
      varArray(nCount) = varCaracterDelimitador & enumStatusConteoFisicoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusConteoFisicoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumStatusRetencionIVACobranzaToString(ByVal statusRetencionIVACobranza As enum_StatusRetencionIVACobranza) As String
   Select Case statusRetencionIVACobranza
      Case eSR_NO_APLICA: enumStatusRetencionIVACobranzaToString = "No Aplica"
      Case eSR_PENDIENTE_POR_DISTRIBUIR: enumStatusRetencionIVACobranzaToString = "Retención del " & gGlobalization.fPromptIVA & " Pendiente de Distribuir"
      Case eSR_DISTRIBUIDO: enumStatusRetencionIVACobranzaToString = "Retención del " & gGlobalization.fPromptIVA & " Distribuida"
      Case Else: enumStatusRetencionIVACobranzaToString = enumStatusRetencionIVACobranzaToString(0)
   End Select
End Function

Public Function strStatusRetencionIVACobranzaToNum(ByVal statusRetencionIVACobranza As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusRetencionIVACobranza
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSR_NO_APLICA To eSR_DISTRIBUIDO
      If UCase(statusRetencionIVACobranza) = UCase(enumStatusRetencionIVACobranzaToString(mEnum)) Then
         strStatusRetencionIVACobranzaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusRetencionIVACobranzaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusRetencionIVACobranza & " no existe en tipo enumerativo statusRetencionIVACobranza"
   End If
h_EXIT:
   Exit Function
End Function

Public Function strFormaDeLaInicialToNum(ByVal formaDeLaInicial As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeLaInicial
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_POR_MONTO To eFD_POR_PORCENTAJE
      If UCase(formaDeLaInicial) = UCase(enumFormaDeLaInicialToString(mEnum)) Then
         strFormaDeLaInicialToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeLaInicialToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeLaInicial & " no existe en tipo enumerativo formaDeLaInicial"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeLaInicial(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumFormaDeLaInicialToString(enum_FormaDeLaInicial.eFD_POR_MONTO)
      End If
      varComboBox.List(nCount) = enumFormaDeLaInicialToString(enum_FormaDeLaInicial.eFD_POR_MONTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeLaInicialToString(enum_FormaDeLaInicial.eFD_POR_PORCENTAJE)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumTipoDeTransaccionDeLibrosFiscalesToString(ByVal tipoDeTransaccionDeLibrosFiscales As enum_TipoDeTransaccionDeLibrosFiscales) As String
   Select Case tipoDeTransaccionDeLibrosFiscales
      Case eTD_A01_REGISTRO: enumTipoDeTransaccionDeLibrosFiscalesToString = "01 Registro"
      Case eTD_A02_COMPLEMENTO: enumTipoDeTransaccionDeLibrosFiscalesToString = "02 Complemento"
      Case eTD_A03_ANULACION: enumTipoDeTransaccionDeLibrosFiscalesToString = "03 Anulación"
      Case eTD_A04_AJUSTE: enumTipoDeTransaccionDeLibrosFiscalesToString = "04 Ajuste"
      Case Else: enumTipoDeTransaccionDeLibrosFiscalesToString = enumTipoDeTransaccionDeLibrosFiscalesToString(0)
   End Select
End Function
Public Function enumTipoDeTransaccionDeLibrosFiscalesToSiglas(ByVal tipoDeTransaccionDeLibrosFiscales As enum_TipoDeTransaccionDeLibrosFiscales) As String
   Select Case tipoDeTransaccionDeLibrosFiscales
      Case eTD_A01_REGISTRO: enumTipoDeTransaccionDeLibrosFiscalesToSiglas = "REG"
      Case eTD_A02_COMPLEMENTO: enumTipoDeTransaccionDeLibrosFiscalesToSiglas = "COM"
      Case eTD_A03_ANULACION: enumTipoDeTransaccionDeLibrosFiscalesToSiglas = "ANL"
      Case eTD_A04_AJUSTE: enumTipoDeTransaccionDeLibrosFiscalesToSiglas = "AJU"
      Case Else: enumTipoDeTransaccionDeLibrosFiscalesToSiglas = enumTipoDeTransaccionDeLibrosFiscalesToSiglas(0)
   End Select
End Function

Public Function strTipoDeTransaccionDeLibrosFiscalesToNum(ByVal tipoDeTransaccionDeLibrosFiscales As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeTransaccionDeLibrosFiscales
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_A01_REGISTRO To eTD_A04_AJUSTE
      If UCase(tipoDeTransaccionDeLibrosFiscales) = UCase(enumTipoDeTransaccionDeLibrosFiscalesToString(mEnum)) Then
         strTipoDeTransaccionDeLibrosFiscalesToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeTransaccionDeLibrosFiscalesToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeTransaccionDeLibrosFiscales & " no existe en tipo enumerativo tipoDeTransaccionDeLibrosFiscales"
   End If
h_EXIT:
   Exit Function
End Function
Public Function strTipoDeTransaccionDeLibrosFiscalesToNumToSiglas(ByVal tipoDeTransaccionDeLibrosFiscales As String) As Integer
   Dim mEnum As enum_TipoDeTransaccionDeLibrosFiscales
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_A01_REGISTRO To eTD_A04_AJUSTE
      If UCase(tipoDeTransaccionDeLibrosFiscales) = UCase(enumTipoDeTransaccionDeLibrosFiscalesToSiglas(mEnum)) Then
         strTipoDeTransaccionDeLibrosFiscalesToNumToSiglas = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeTransaccionDeLibrosFiscalesToNumToSiglas = 0
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithTipoDeTransaccionDeLibrosFiscales(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeTransaccionDeLibrosFiscalesToString(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO)
      End If
      varComboBox.List(nCount) = enumTipoDeTransaccionDeLibrosFiscalesToString(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeTransaccionDeLibrosFiscalesToString(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A02_COMPLEMENTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeTransaccionDeLibrosFiscalesToString(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A03_ANULACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeTransaccionDeLibrosFiscalesToString(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function enumTipoDeProveedorDeLibrosFiscalesToString(ByVal tipoDeProveedorDeLibrosFiscales As enum_TipoDeProveedorDeLibrosFiscales) As String
   Select Case tipoDeProveedorDeLibrosFiscales
      Case eTD_CON_RIF: enumTipoDeProveedorDeLibrosFiscalesToString = "Con " & gGlobalization.fPromptRIF
      Case eTD_SIN_RIF: enumTipoDeProveedorDeLibrosFiscalesToString = "Sin " & gGlobalization.fPromptRIF
      Case eTD_NO_RESIDENCIADO: enumTipoDeProveedorDeLibrosFiscalesToString = "No Residenciado"
      Case eTD_NO_DOMICILIADO: enumTipoDeProveedorDeLibrosFiscalesToString = "No Domiciliado"
      Case Else: enumTipoDeProveedorDeLibrosFiscalesToString = enumTipoDeProveedorDeLibrosFiscalesToString(0)
   End Select
End Function

Public Function strTipoDeProveedorDeLibrosFiscalesToNum(ByVal tipoDeProveedorDeLibrosFiscales As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeProveedorDeLibrosFiscales
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_CON_RIF To eTD_NO_DOMICILIADO
      If UCase(tipoDeProveedorDeLibrosFiscales) = UCase(enumTipoDeProveedorDeLibrosFiscalesToString(mEnum)) Then
         strTipoDeProveedorDeLibrosFiscalesToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeProveedorDeLibrosFiscalesToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeProveedorDeLibrosFiscales & " no existe en tipo enumerativo tipoDeProveedorDeLibrosFiscales"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeProveedorDeLibrosFiscales(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDeProveedorDeLibrosFiscalesToString(enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF)
      End If
      varComboBox.List(nCount) = enumTipoDeProveedorDeLibrosFiscalesToString(enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeProveedorDeLibrosFiscalesToString(enum_TipoDeProveedorDeLibrosFiscales.eTD_SIN_RIF)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeProveedorDeLibrosFiscalesToString(enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_RESIDENCIADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDeProveedorDeLibrosFiscalesToString(enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Sub FillComboBoxWithPorcentajeDeRetencionDeIVA(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim por0 As String
   Dim por75 As String
   Dim por100 As String
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      por0 = "0"
      por75 = "75"
      por100 = "100"
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = por0
      End If
      varComboBox.List(nCount) = por0
      nCount = nCount + 1
      varComboBox.List(nCount) = por75
      nCount = nCount + 1
      varComboBox.List(nCount) = por100
      varComboBox.ListIndex = 1
      varComboBox.Text = por75
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function enumTipoDeVentaToString(ByVal tipoDeVenta As enum_TipoDeVenta) As String
   Select Case tipoDeVenta
      Case eTD_INTERNA: enumTipoDeVentaToString = "Interna"
      Case eTD_EXPORTACION: enumTipoDeVentaToString = "Exportacion"
      Case eTD_SIN_DERECHO_A_CREDITO_FISCAL: enumTipoDeVentaToString = "Sin Derecho a Crédito Fiscal"
      Case eTD_A_CONTRIBUYENTE: enumTipoDeVentaToString = "A Contribuyente"
      Case eTD_A_NO_CONTRIBUYENTE: enumTipoDeVentaToString = "A No Contribuyente"
      Case Else: enumTipoDeVentaToString = enumTipoDeVentaToString(0)
   End Select
End Function

Public Function strTipoDeVentaToNum(ByVal tipoDeVenta As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeVenta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_INTERNA To eTD_A_NO_CONTRIBUYENTE
      If UCase(tipoDeVenta) = UCase(enumTipoDeVentaToString(mEnum)) Then
         strTipoDeVentaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeVentaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeVenta & " no existe en tipo enumerativo tipoDeVenta"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeVenta(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeVentaToString(enum_TipoDeVenta.eTD_INTERNA)
   End If
   varComboBox.List(nCount) = enumTipoDeVentaToString(enum_TipoDeVenta.eTD_INTERNA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeVentaToString(enum_TipoDeVenta.eTD_EXPORTACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeVentaToString(enum_TipoDeVenta.eTD_SIN_DERECHO_A_CREDITO_FISCAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeVentaToString(enum_TipoDeVenta.eTD_A_CONTRIBUYENTE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeVentaToString(enum_TipoDeVenta.eTD_A_NO_CONTRIBUYENTE)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function fEnumTransaccionFiscalToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeTransaccionDeLibrosFiscales
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE - enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO To enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTipoDeTransaccionDeLibrosFiscalesToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTipoDeTransaccionDeLibrosFiscalesToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumTransaccionFiscalToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumProveedorFiscalToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeProveedorDeLibrosFiscales
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO - enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF To enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO
      varArray(nCount) = varCaracterDelimitador & enumTipoDeProveedorDeLibrosFiscalesToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumProveedorFiscalToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumDondeSeEfectuaLaRetencionIVAToString(ByVal dondeSeEfectuaLaRetencionIVA As enum_DondeSeEfectuaLaRetencionIVA) As String
   Select Case dondeSeEfectuaLaRetencionIVA
      Case eDS_NO_RETENIDA: enumDondeSeEfectuaLaRetencionIVAToString = "No Asignado - No Retenida"
      Case eDS_CXP: enumDondeSeEfectuaLaRetencionIVAToString = "CxP"
      Case eDS_PAGO: enumDondeSeEfectuaLaRetencionIVAToString = "Pago"
      Case Else: enumDondeSeEfectuaLaRetencionIVAToString = enumDondeSeEfectuaLaRetencionIVAToString(0)
   End Select
End Function

Public Function enumDondeSeEfectuaLaRetencionIVAToSiglas(ByVal dondeSeEfectuaLaRetencionIVA As enum_DondeSeEfectuaLaRetencionIVA) As String
   Select Case dondeSeEfectuaLaRetencionIVA
      Case eDS_NO_RETENIDA: enumDondeSeEfectuaLaRetencionIVAToSiglas = "N/R"
      Case eDS_CXP: enumDondeSeEfectuaLaRetencionIVAToSiglas = "CxP"
      Case eDS_PAGO: enumDondeSeEfectuaLaRetencionIVAToSiglas = "PAG"
      Case Else: enumDondeSeEfectuaLaRetencionIVAToSiglas = enumDondeSeEfectuaLaRetencionIVAToSiglas(0)
   End Select
End Function

Public Function strDondeSeEfectuaLaRetencionIVAToNum(ByVal dondeSeEfectuaLaRetencionIVA As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_DondeSeEfectuaLaRetencionIVA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eDS_NO_RETENIDA To eDS_PAGO
      If UCase(dondeSeEfectuaLaRetencionIVA) = UCase(enumDondeSeEfectuaLaRetencionIVAToString(mEnum)) Then
         strDondeSeEfectuaLaRetencionIVAToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strDondeSeEfectuaLaRetencionIVAToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & dondeSeEfectuaLaRetencionIVA & " no existe en tipo enumerativo dondeSeEfectuaLaRetencionIVA"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithDondeSeEfectuaLaRetencionIVA(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumDondeSeEfectuaLaRetencionIVAToString(enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA)
      End If
      varComboBox.List(nCount) = enumDondeSeEfectuaLaRetencionIVAToString(enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDondeSeEfectuaLaRetencionIVAToString(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumDondeSeEfectuaLaRetencionIVAToString(enum_DondeSeEfectuaLaRetencionIVA.eDS_PAGO)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function enumDondeEfectuoContabilizacionRetIVAToString(DondeEfectuoContabilizacionRetIVA As enum_DondeEfectuoContabilizacionRetIVA) As String
   Select Case DondeEfectuoContabilizacionRetIVA
      Case eDC_NoContabilizada: enumDondeEfectuoContabilizacionRetIVAToString = "No Contabilizada"
      Case eDC_CxP: enumDondeEfectuoContabilizacionRetIVAToString = "C x P"
      Case eDC_Pago: enumDondeEfectuoContabilizacionRetIVAToString = "Pago"
      Case Else
         enumDondeEfectuoContabilizacionRetIVAToString = enumDondeEfectuoContabilizacionRetIVAToString(0)
   End Select
End Function

Public Function strDondeEfectuoContabilizacionRetIVAToNum(ByVal DondeEfectuoContabilizacionRetIVA As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_DondeEfectuoContabilizacionRetIVA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eDC_NoContabilizada To eDC_Pago
      If UCase(DondeEfectuoContabilizacionRetIVA) = UCase(enumDondeEfectuoContabilizacionRetIVAToString(mEnum)) Then
         strDondeEfectuoContabilizacionRetIVAToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strDondeEfectuoContabilizacionRetIVAToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & DondeEfectuoContabilizacionRetIVA & " no existe en tipo enumerativo DondeEfectuoContabilizacionRetIVA"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithDondeEfectuoContabilizacionRetIVA(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumDondeEfectuoContabilizacionRetIVAToString(enum_DondeEfectuoContabilizacionRetIVA.eDC_NoContabilizada)
   End If
   varComboBox.List(nCount) = enumDondeEfectuoContabilizacionRetIVAToString(enum_DondeEfectuoContabilizacionRetIVA.eDC_NoContabilizada)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDondeEfectuoContabilizacionRetIVAToString(enum_DondeEfectuoContabilizacionRetIVA.eDC_CxP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumDondeEfectuoContabilizacionRetIVAToString(enum_DondeEfectuoContabilizacionRetIVA.eDC_Pago)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumFormaDeReiniciarComprobanteRetIVAToString(ByVal formaDeReiniciarComprobanteRetIVA As enum_FormaDeReiniciarComprobanteRetIVA) As String
   Select Case formaDeReiniciarComprobanteRetIVA
      Case eFD_SIN_ESCOGER: enumFormaDeReiniciarComprobanteRetIVAToString = "Sin Escoger"
      Case eFD_POR_MES: enumFormaDeReiniciarComprobanteRetIVAToString = "Por Mes"
      Case eFD_POR_ANO: enumFormaDeReiniciarComprobanteRetIVAToString = "Por Año"
      Case eFD_AL_COMPLETAR: enumFormaDeReiniciarComprobanteRetIVAToString = "Al Completar"
      Case Else: enumFormaDeReiniciarComprobanteRetIVAToString = enumFormaDeReiniciarComprobanteRetIVAToString(0)
   End Select
End Function

Public Function strFormaDeReiniciarComprobanteRetIVAToNum(ByVal formaDeReiniciarComprobanteRetIVA As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeReiniciarComprobanteRetIVA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_SIN_ESCOGER To eFD_AL_COMPLETAR
      If UCase(formaDeReiniciarComprobanteRetIVA) = UCase(enumFormaDeReiniciarComprobanteRetIVAToString(mEnum)) Then
         strFormaDeReiniciarComprobanteRetIVAToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeReiniciarComprobanteRetIVAToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeReiniciarComprobanteRetIVA & " no existe en tipo enumerativo formaDeReiniciarComprobanteRetIVA"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeReiniciarComprobanteRetIVA(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumFormaDeReiniciarComprobanteRetIVAToString(enum_FormaDeReiniciarComprobanteRetIVA.eFD_SIN_ESCOGER)
      End If
      varComboBox.List(nCount) = enumFormaDeReiniciarComprobanteRetIVAToString(enum_FormaDeReiniciarComprobanteRetIVA.eFD_SIN_ESCOGER)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeReiniciarComprobanteRetIVAToString(enum_FormaDeReiniciarComprobanteRetIVA.eFD_POR_MES)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeReiniciarComprobanteRetIVAToString(enum_FormaDeReiniciarComprobanteRetIVA.eFD_POR_ANO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumFormaDeReiniciarComprobanteRetIVAToString(enum_FormaDeReiniciarComprobanteRetIVA.eFD_AL_COMPLETAR)
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      End If
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function enumTipoDeContribuyenteIvaToString(ByVal tipoDeContribuyenteDelIva As enum_TipoDeContribuyenteIVA) As String
   Select Case tipoDeContribuyenteDelIva
      Case eTD_FORMAL: enumTipoDeContribuyenteIvaToString = "Contribuyente Formal"
      Case eTD_ORDINARIOS: enumTipoDeContribuyenteIvaToString = "Contribuyente Ordinario"
      Case eTD_ESPECIAL: enumTipoDeContribuyenteIvaToString = "Contribuyente Especial"
      Case eTD_ENTES_PUBLICOS: enumTipoDeContribuyenteIvaToString = "Entes Publicos"
      Case eTD_CONVERSION: enumTipoDeContribuyenteIvaToString = "Conversión"
      Case Else: enumTipoDeContribuyenteIvaToString = enumTipoDeContribuyenteIvaToString(0)
   End Select
End Function

Public Function strTipoDeContribuyenteIvaToNum(ByVal tipoDeContribuyenteDelIva As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeContribuyenteIVA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_FORMAL To eTD_CONVERSION
      If UCase(tipoDeContribuyenteDelIva) = UCase(enumTipoDeContribuyenteIvaToString(mEnum)) Then
         strTipoDeContribuyenteIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeContribuyenteIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeContribuyenteDelIva & " no existe en tipo enumerativo tipoDeContribuyenteDelIva"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumStatusGestionToString(ByVal statusGestion As enum_StatusGestion) As String
   Select Case statusGestion
      Case eSG_PENDIENTE: enumStatusGestionToString = "Pendiente"
      Case eSG_REALIZADA: enumStatusGestionToString = "Realizada"
      Case eSG_FINALIZADA: enumStatusGestionToString = "Finalizada"
      Case Else: enumStatusGestionToString = enumStatusGestionToString(0)
   End Select
End Function

Public Function strStatusGestionToNum(ByVal statusGestion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusGestion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSG_PENDIENTE To eSG_FINALIZADA
      If UCase(statusGestion) = UCase(enumStatusGestionToString(mEnum)) Then
         strStatusGestionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusGestionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusGestion & " no existe en tipo enumerativo statusGestion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusGestion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      varComboBox.Clear
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusGestionToString(enum_StatusGestion.eSG_PENDIENTE)
      End If
      varComboBox.List(nCount) = enumStatusGestionToString(enum_StatusGestion.eSG_PENDIENTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusGestionToString(enum_StatusGestion.eSG_REALIZADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusGestionToString(enum_StatusGestion.eSG_FINALIZADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0
   End If
End Sub

Public Function enumProximoPasoGestionToString(ByVal proximoPasoGestion As enum_ProximoPasoGestion) As String
   Select Case proximoPasoGestion
      Case ePP_INDICAR: enumProximoPasoGestionToString = "Indicar"
      Case ePP_LLAMAR: enumProximoPasoGestionToString = "Llamar"
      Case ePP_COBRAR_MENSAJERO: enumProximoPasoGestionToString = "Cobrar Mensajero"
      Case ePP_COBRAR_Vendedor: enumProximoPasoGestionToString = "Cobrar Vendedor"
      Case ePP_RECLAMO: enumProximoPasoGestionToString = "Reclamo"
      Case Else: enumProximoPasoGestionToString = enumProximoPasoGestionToString(0)
   End Select
End Function

Public Function strProximoPasoGestionToNum(ByVal proximoPasoGestion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ProximoPasoGestion
   Dim nCount As Integer
   nCount = 0
   For mEnum = ePP_INDICAR To ePP_RECLAMO
      If UCase(proximoPasoGestion) = UCase(enumProximoPasoGestionToString(mEnum)) Then
         strProximoPasoGestionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strProximoPasoGestionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & proximoPasoGestion & " no existe en tipo enumerativo proximoPasoGestion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithProximoPasoGestion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumProximoPasoGestionToString(enum_ProximoPasoGestion.ePP_INDICAR)
   End If
   varComboBox.List(nCount) = enumProximoPasoGestionToString(enum_ProximoPasoGestion.ePP_INDICAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumProximoPasoGestionToString(enum_ProximoPasoGestion.ePP_LLAMAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumProximoPasoGestionToString(enum_ProximoPasoGestion.ePP_COBRAR_MENSAJERO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumProximoPasoGestionToString(enum_ProximoPasoGestion.ePP_COBRAR_Vendedor)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumProximoPasoGestionToString(enum_ProximoPasoGestion.ePP_RECLAMO)
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0
End Sub

Public Function fEnumProximoPasoGestionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_ProximoPasoGestion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_ProximoPasoGestion.ePP_RECLAMO - enum_ProximoPasoGestion.ePP_INDICAR
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = ePP_INDICAR To ePP_RECLAMO
      varArray(nCount) = varCaracterDelimitador & enumProximoPasoGestionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumProximoPasoGestionToStringInArray = varArray
   On Error GoTo 0
End Function


Public Function fenumStatusConciliacionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusConciliacion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusConciliacion.eSC_CERRADA - enum_StatusConciliacion.eSC_ENPROCESO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSC_ENPROCESO To eSC_CERRADA
      varArray(nCount) = varCaracterDelimitador & enumStatusConciliacionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumStatusConciliacionToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function strPeriodoToNum(ByVal periodo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Periodo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eP_Mensual To eP_Semestral
      If UCase(periodo) = UCase(enumPeriodoToString(mEnum)) Then
         strPeriodoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strPeriodoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & periodo & " no existe en tipo enumerativo periodo"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumPeriodoToString(ByVal periodo As enum_Periodo) As String
   Select Case periodo
      Case eP_Mensual: enumPeriodoToString = "Mensual"
      Case eP_Trimestral: enumPeriodoToString = "Trimestral"
      Case eP_Semestral: enumPeriodoToString = "Semestral"
      Case Else: enumPeriodoToString = enumPeriodoToString(0)
   End Select
End Function

Public Function EnumTablaCiiuToString(ByVal tablaCiiu As Enum_TablaCiiu) As String
   Select Case tablaCiiu
      Case eTC_Ninguna: EnumTablaCiiuToString = "Ninguno"
      Case eTC_TablaAnteriorA2004: EnumTablaCiiuToString = "Tabla C.I.I.U. Anterior al 2004"
      Case eTC_Tabla2004: EnumTablaCiiuToString = "Tabla C.I.I.U 2004"
      Case Else: EnumTablaCiiuToString = EnumTablaCiiuToString(2)
   End Select
End Function

Public Function strTablaCiiuToNum(ByVal tablaCiiu As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As Enum_TablaCiiu
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTC_Ninguna To eTC_Tabla2004
    If UCase(tablaCiiu) = UCase(EnumTablaCiiuToString(mEnum)) Then
         strTablaCiiuToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTablaCiiuToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tablaCiiu & " no existe en tipo enumerativo TablaCiiu"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTablaCiiu(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      varComboBox.Clear
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = EnumTablaCiiuToString(Enum_TablaCiiu.eTC_TablaAnteriorA2004)
      End If
      varComboBox.List(nCount) = EnumTablaCiiuToString(Enum_TablaCiiu.eTC_TablaAnteriorA2004)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTablaCiiuToString(Enum_TablaCiiu.eTC_Tabla2004)
      If action = Buscar Then
         varComboBox.ListIndex = 0
      Else
         varComboBox.ListIndex = 1  ' Se selecciona el valor por defecto
      End If
   End If
End Sub

Public Function enumModeloPlanillaForma00030ToString(ByVal ModeloPlanillaForma00030 As enum_ModeloPlanillaForma00030) As String
   Select Case ModeloPlanillaForma00030
      Case eMPF00030_F03_Grafibond: enumModeloPlanillaForma00030ToString = "Forma 00030 F-03 Grafibond"
      Case eMPF00030_F03_Olivenca: enumModeloPlanillaForma00030ToString = "Forma 00030 F-03 Olivenca"
      Case eMPF00030_F04_Grafibond: enumModeloPlanillaForma00030ToString = "Forma 00030 F-04 Grafibond"
      Case Else: enumModeloPlanillaForma00030ToString = enumModeloPlanillaForma00030ToString(0)
      End Select
End Function

Public Function strModeloPlanillaForma00030ToNum(ByVal ModeloPlanillaForma00030 As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ModeloPlanillaForma00030
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMPF00030_F03_Grafibond To eMPF00030_F04_Grafibond
      If UCase(ModeloPlanillaForma00030) = UCase(enumModeloPlanillaForma00030ToString(mEnum)) Then
         strModeloPlanillaForma00030ToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strModeloPlanillaForma00030ToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ModeloPlanillaForma00030 & " no existe en tipo enumerativo ModeloPlanillaForma00030"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithModeloPlanillaForma00030(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      varComboBox.Clear
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumModeloPlanillaForma00030ToString(enum_ModeloPlanillaForma00030.eMPF00030_F03_Grafibond)
      End If
      varComboBox.List(nCount) = enumModeloPlanillaForma00030ToString(enum_ModeloPlanillaForma00030.eMPF00030_F03_Grafibond)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumModeloPlanillaForma00030ToString(enum_ModeloPlanillaForma00030.eMPF00030_F03_Olivenca)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumModeloPlanillaForma00030ToString(enum_ModeloPlanillaForma00030.eMPF00030_F04_Grafibond)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   End If
End Sub

Public Function fEnumTipoDeTransaccioToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeTransaccion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL - enum_TipoDeTransaccion.eTD_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeTransaccion.eTD_FACTURA To enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTipoDeCxCToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTipoDeCxCToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumTipoDeTransaccioToStringInArray = varArray
   On Error GoTo 0
End Function

'Public Function fEnumNombreProductoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
'   Dim varEnum As enum_NombreProducto
'   Dim varArray() As String
'   Dim varLimiteSuperior As Integer
'   Dim nCount As Integer
'   Dim varCaracterDelimitador As String
'   On Error Resume Next
'   varCaracterDelimitador = ""
'   If valPrepararParaSql Then
'      varCaracterDelimitador = "'"
'   End If
'   'varLimiteSuperior = enum_NombreProducto.eNP_IVAGALAC - enum_NombreProducto.eNP_IVA
'   varLimiteSuperior = enum_NombreProducto.eNP_PRESUPUESTO - enum_NombreProducto.eNP_IVA
'   nCount = 0
'   ReDim varArray(0 To varLimiteSuperior)
''   For varEnum = enum_NombreProducto.eNP_IVA To enum_NombreProducto.eNP_IVAGALAC
'   For varEnum = enum_NombreProducto.eNP_IVA To enum_NombreProducto.eNP_PRESUPUESTO
'      varArray(nCount) = varCaracterDelimitador & enumNombreProductoToString(varEnum) & varCaracterDelimitador
'      nCount = nCount + 1
'   Next
'   fEnumNombreProductoToStringInArray = varArray
'   On Error GoTo 0
'End Function

Public Function fEnumTipoDeProductoToStringInArray() As Variant
   Dim varEnum As enum_TipoDeProducto
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = "'"
   varLimiteSuperior = enum_TipoDeProducto.eTD_TP_POLIZA - enum_TipoDeProducto.eTD_TP_NUEVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTD_TP_NUEVO To eTD_TP_POLIZA
      varArray(nCount) = varCaracterDelimitador & enumTipoDeProductoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeProductoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumStatusComprasToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusCompra
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusCompra.eSCp_ANULADA - enum_StatusCompra.eSCp_VIGENTE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusCompra.eSCp_VIGENTE To enum_StatusCompra.eSCp_ANULADA
      varArray(nCount) = varCaracterDelimitador & enumStatusCompraToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusComprasToStringInArray = varArray
   On Error GoTo 0
End Function

Public Sub FillComboBoxWithStatusFacturaParaImprmirNotaDeEntrega(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusFacturaToString(enum_StatusFactura.eSF_EMITIDA)
      End If
      varComboBox.List(nCount) = enumStatusFacturaToString(enum_StatusFactura.eSF_EMITIDA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusFacturaToString(enum_StatusFactura.eSF_BORRADOR)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumOrigenRendicionToString(OrigenRendicion As enum_OrigenRendicion) As String
   Select Case OrigenRendicion
      Case eOR_Manual: enumOrigenRendicionToString = "Manual"
      Case eOR_Sistema10: enumOrigenRendicionToString = "Sistema 10"
      Case Else
         enumOrigenRendicionToString = enumOrigenRendicionToString(0)
   End Select
End Function
Public Function strOrigenRendicionToNum(ByVal OrigenRendicion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_OrigenRendicion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eOR_Manual To eOR_Sistema10
      If UCase(OrigenRendicion) = UCase(enumOrigenRendicionToString(mEnum)) Then
         strOrigenRendicionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strOrigenRendicionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & OrigenRendicion & " no existe en tipo enumerativo OrigenRendicion"
   End If
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithOrigenRendicion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumOrigenRendicionToString(enum_OrigenRendicion.eOR_Manual)
   End If
   varComboBox.List(nCount) = enumOrigenRendicionToString(enum_OrigenRendicion.eOR_Manual)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumOrigenRendicionToString(enum_OrigenRendicion.eOR_Sistema10)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumStatusMaquinaFiscalToString(ByVal statusMaquinaFiscal As enum_StatusMaquinaFiscal) As String
   Select Case statusMaquinaFiscal
      Case eSMF_ACTIVA: enumStatusMaquinaFiscalToString = "Activa"
      Case eSMF_INACTIVA: enumStatusMaquinaFiscalToString = "Inactiva"
      Case Else: enumStatusMaquinaFiscalToString = enumStatusMaquinaFiscalToString(0)
   End Select
End Function

Public Function strStatusMaquinaFiscalToNum(ByVal statusMaquinaFiscal As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusMaquinaFiscal
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSMF_ACTIVA To eSMF_INACTIVA
      If UCase(statusMaquinaFiscal) = UCase(enumStatusMaquinaFiscalToString(mEnum)) Then
         strStatusMaquinaFiscalToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusMaquinaFiscalToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusMaquinaFiscal & " no existe en tipo enumerativo statusMaquinaFiscal"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusMaquinaFiscal(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusMaquinaFiscalToString(enum_StatusMaquinaFiscal.eSMF_ACTIVA)
   End If
   varComboBox.List(nCount) = enumStatusMaquinaFiscalToString(enum_StatusMaquinaFiscal.eSMF_ACTIVA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusMaquinaFiscalToString(enum_StatusMaquinaFiscal.eSMF_INACTIVA)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function fenumTipoDeVentaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeVenta
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeVenta.eTD_A_NO_CONTRIBUYENTE - enum_TipoDeVenta.eTD_INTERNA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTD_INTERNA To eTD_A_NO_CONTRIBUYENTE
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTipoDeVentaToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTipoDeVentaToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fenumTipoDeVentaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeVentaToStringSiglas(tipoDeVenta As enum_TipoDeVenta) As String
   Select Case tipoDeVenta
      Case eTD_INTERNA: enumTipoDeVentaToStringSiglas = "VEI"
      Case eTD_EXPORTACION: enumTipoDeVentaToStringSiglas = "VEP"
      Case eTD_SIN_DERECHO_A_CREDITO_FISCAL: enumTipoDeVentaToStringSiglas = "SDC"
      Case eTD_A_CONTRIBUYENTE: enumTipoDeVentaToStringSiglas = "VEC"
      Case eTD_A_NO_CONTRIBUYENTE: enumTipoDeVentaToStringSiglas = "VNC"
      Case Else: enumTipoDeVentaToStringSiglas = enumTipoDeVentaToStringSiglas(0)
   End Select
End Function
Public Function enumResumenDeLibrosToString(ByVal valResumenDeLibros As enum_ResumenDeLibros) As String
   Select Case valResumenDeLibros
      Case eRDL_SOLO_VENTAS: enumResumenDeLibrosToString = "Solo Ventas"
      Case eRDL_SOLO_COMPRAS: enumResumenDeLibrosToString = "Solo Compras"
      Case eRDL_VENTAS_Y_COMPRAS: enumResumenDeLibrosToString = "Compras y Ventas"
      Case Else: enumResumenDeLibrosToString = enumResumenDeLibrosToString(0)
   End Select
End Function

Public Function strResumenDeLibrosToNum(ByVal valResumenDeLibros As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ResumenDeLibros
   Dim nCount As Integer
   nCount = 0
   For mEnum = eRDL_SOLO_VENTAS To eRDL_VENTAS_Y_COMPRAS
      If UCase(valResumenDeLibros) = UCase(enumStatusMaquinaFiscalToString(mEnum)) Then
         strResumenDeLibrosToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strResumenDeLibrosToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valResumenDeLibros & " no existe en tipo enumerativo statusMaquinaFiscal"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithResumenDeLibrosParaVentas(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumResumenDeLibrosToString(enum_ResumenDeLibros.eRDL_SOLO_VENTAS)
   End If
   varComboBox.List(nCount) = enumResumenDeLibrosToString(enum_ResumenDeLibros.eRDL_SOLO_VENTAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumResumenDeLibrosToString(enum_ResumenDeLibros.eRDL_VENTAS_Y_COMPRAS)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoArticuloInvToString(TipoArticuloInv As enum_TipoArticuloInv) As String
   Select Case TipoArticuloInv
      Case eTAI_Simple: enumTipoArticuloInvToString = "Simple"
      Case eTAI_UsaTalla_Color: enumTipoArticuloInvToString = "Usa  Talla /Color"
      Case eTAI_UsaSerial: enumTipoArticuloInvToString = "Usa  Serial"
      Case eTAI_UsaTalla_ColorySerial: enumTipoArticuloInvToString = "Usa  Talla /Color y  Serial"
      Case eTAI_UsaSerial_Rollo: enumTipoArticuloInvToString = "Usa Serial y Rollo"
      Case Else
         enumTipoArticuloInvToString = enumTipoArticuloInvToString(0)
   End Select
End Function

Public Function strTipoArticuloInvToNum(ByVal TipoArticuloInv As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoArticuloInv
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTAI_Simple To eTAI_UsaSerial_Rollo
      If UCase(TipoArticuloInv) = UCase(enumTipoArticuloInvToString(mEnum)) Then
         strTipoArticuloInvToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoArticuloInvToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoArticuloInv & " no existe en tipo enumerativo TipoArticuloInv"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoArticuloInv(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoArticuloInvToString(enum_TipoArticuloInv.eTAI_Simple)
   End If
   varComboBox.List(nCount) = enumTipoArticuloInvToString(enum_TipoArticuloInv.eTAI_Simple)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoArticuloInvToString(enum_TipoArticuloInv.eTAI_UsaTalla_Color)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoArticuloInvToString(enum_TipoArticuloInv.eTAI_UsaSerial)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoArticuloInvToString(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoArticuloInvToString(enum_TipoArticuloInv.eTAI_UsaSerial_Rollo)
   varComboBox.Width = 2280
   'varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
End Sub

Public Function fEnumTipoArticuloInvToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoArticuloInv
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoArticuloInv.eTAI_UsaSerial_Rollo - enum_TipoArticuloInv.eTAI_Simple
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoArticuloInv.eTAI_Simple To enum_TipoArticuloInv.eTAI_UsaSerial_Rollo
      varArray(nCount) = varCaracterDelimitador & enumTipoArticuloInvToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoArticuloInvToStringInArray = varArray
   On Error GoTo 0
End Function


Public Sub FillComboBoxWithResumenDeLibrosParaCompras(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
         varComboBox.Text = enumResumenDeLibrosToString(enum_ResumenDeLibros.eRDL_SOLO_COMPRAS)
   End If
   varComboBox.List(nCount) = enumResumenDeLibrosToString(enum_ResumenDeLibros.eRDL_SOLO_COMPRAS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumResumenDeLibrosToString(enum_ResumenDeLibros.eRDL_VENTAS_Y_COMPRAS)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumProductoLlamadaToString(ByVal nombreProducto As enum_ProductoLlamada) As String
   Select Case nombreProducto
      Case eNPLL_IVA: enumProductoLlamadaToString = "IVA"
      Case eNPLL_AXI: enumProductoLlamadaToString = "AXI"
      Case eNPLL_INFOCONT: enumProductoLlamadaToString = "INFOCONT"
      Case eNPLL_DPN: enumProductoLlamadaToString = "DPN"
      Case eNPLL_DPJ: enumProductoLlamadaToString = "DPJ"
      Case eNPLL_FACTURA: enumProductoLlamadaToString = "FACTURA"
      Case eNPLL_AXP: enumProductoLlamadaToString = "AXP"
      Case eNPLL_NOMINA: enumProductoLlamadaToString = "NOMINA"
      Case eNPLL_RET: enumProductoLlamadaToString = "RET"
      Case eNPLL_WCONTABILIDAD: enumProductoLlamadaToString = "WINCONT"
      Case eNPLL_ISV: enumProductoLlamadaToString = "ISV"
      Case eNPLL_ADMInterno: enumProductoLlamadaToString = "ADI"
      Case eNPLL_SAW: enumProductoLlamadaToString = "SAW"
      Case eNPLL_WINIVA: enumProductoLlamadaToString = "WINIVA"
      Case eNPLL_WINRET: enumProductoLlamadaToString = "WINRET"
      Case eNPLL_COMBO: enumProductoLlamadaToString = "COMBO"
      Case eNPLL_SERVICIO: enumProductoLlamadaToString = "SERVICIO"
      Case eNPLL_SAWINTEGRADO: enumProductoLlamadaToString = "SAW Integrado"
      Case eNPLL_IVAGALAC: enumProductoLlamadaToString = "IVA Gálac"
      Case eNPLL_DER: enumProductoLlamadaToString = "DER"
      Case eNPLL_PRESUPUESTO: enumProductoLlamadaToString = "PRE"
      Case eNPLL_NOMINA_WINDOWS: enumProductoLlamadaToString = "NOMINA WINDOWS"
      Case Else: enumProductoLlamadaToString = enumProductoLlamadaToString(0)
   End Select
End Function

Public Function strProductoLlamadaToNum(ByVal ProductoLlamada As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ProductoLlamada
   Dim nCount As Integer
   nCount = 0
   For mEnum = eNPLL_IVA To eNPLL_NOMINA_WINDOWS
   If UCase(ProductoLlamada) = UCase(enumProductoLlamadaToString(mEnum)) Then
      strProductoLlamadaToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strProductoLlamadaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ProductoLlamada & " no existe en tipo enumerativo ProductoLlamada"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithProductoLlamada(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_IVA)
      End If
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_IVA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_AXI)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_INFOCONT)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_DPN)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_DPJ)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_FACTURA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_AXP)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_NOMINA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_RET)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_WCONTABILIDAD)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_ISV)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_ADMInterno)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_SAW)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_WINIVA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_WINRET)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_COMBO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_SERVICIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_SAWINTEGRADO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_IVAGALAC)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_DER)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_PRESUPUESTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumProductoLlamadaToString(enum_ProductoLlamada.eNPLL_NOMINA_WINDOWS)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function enumAgruparLibroDeVentasToString(ByVal valAgruparLibroDeVentas As enum_AgruparLibroDeVentas) As String
   Select Case valAgruparLibroDeVentas
      Case eALDV_NINGUNO: enumAgruparLibroDeVentasToString = "Ninguno"
      Case eALDV_TALONARIO: enumAgruparLibroDeVentasToString = "Talonario"
      Case eALDV_MAQUINA: enumAgruparLibroDeVentasToString = "Máquina"
      Case eALDV_TALONARIO_Y_MAQUINA: enumAgruparLibroDeVentasToString = "Talonario y Máquina"
      Case eALDV_MAQUINA_Y_TALONARIO: enumAgruparLibroDeVentasToString = "Máquina y Talonario"
      Case Else: enumAgruparLibroDeVentasToString = enumAgruparLibroDeVentasToString(0)
   End Select
End Function

Public Function strAgruparLibroDeVentasToNum(ByVal valAgruparLibroDeVentas As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AgruparLibroDeVentas
   Dim nCount As Integer
   nCount = 0
   For mEnum = eALDV_NINGUNO To eALDV_MAQUINA_Y_TALONARIO
      If UCase(valAgruparLibroDeVentas) = UCase(enumAgruparLibroDeVentasToString(mEnum)) Then
         strAgruparLibroDeVentasToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAgruparLibroDeVentasToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & strAgruparLibroDeVentasToNum & " no existe en tipo enumerativo AgruparLibroDeVentas"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithAgruparLibroDeVentasTodos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_NINGUNO)
      End If
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_NINGUNO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_TALONARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_MAQUINA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_TALONARIO_Y_MAQUINA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_MAQUINA_Y_TALONARIO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Sub FillComboBoxWithAgruparLibroDeVentasSoloTalonario(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_NINGUNO)
      End If
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_NINGUNO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_TALONARIO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Sub FillComboBoxWithAgruparLibroDeVentasSoloMaquinaFiscal(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_NINGUNO)
      End If
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_NINGUNO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumAgruparLibroDeVentasToString(enum_AgruparLibroDeVentas.eALDV_MAQUINA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumStatusArticuloToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusArticulo
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusArticulo.eSA_DESINCORPORADO - enum_StatusArticulo.eSA_VIGENTE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusArticulo.eSA_VIGENTE To enum_StatusArticulo.eSA_DESINCORPORADO
      varArray(nCount) = varCaracterDelimitador & enumStatusArticuloToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusArticuloToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumProductoLLamadaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_ProductoLlamada
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_ProductoLlamada.eNPLL_NOMINA_WINDOWS - enum_ProductoLlamada.eNPLL_IVA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_ProductoLlamada.eNPLL_IVA To enum_ProductoLlamada.eNPLL_NOMINA_WINDOWS
      varArray(nCount) = varCaracterDelimitador & enumProductoLlamadaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumProductoLLamadaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumEntidadAExportarOImportarToString(ByVal EntidadAExportarOImportar As enum_EntidadAExportarOImportar) As String
   Select Case EntidadAExportarOImportar
      Case eEAEI_PROVEEDOR: enumEntidadAExportarOImportarToString = "Proveedores"
      Case eEAEI_CLIENTE: enumEntidadAExportarOImportarToString = "Clientes"
      Case eEAEI_ARTICULO_INVENTARIO: enumEntidadAExportarOImportarToString = "Artículos de Inventario"
      Case eEAEI_CxC: enumEntidadAExportarOImportarToString = "Cuentas por Cobrar"
      Case eEAEI_CxP: enumEntidadAExportarOImportarToString = "Cuentas por Pagar"
      Case eEAEI_FACTURA: enumEntidadAExportarOImportarToString = "Facturas"
      Case eEAEI_VENDEDOR: enumEntidadAExportarOImportarToString = "Vendedores"
      Case eEAEI_MERCANTIL: enumEntidadAExportarOImportarToString = "Mercantil"
      Case eEAEI_BOD: enumEntidadAExportarOImportarToString = "BOD"
      Case eEAEI_COTIZACION: enumEntidadAExportarOImportarToString = "Cotizacion"
      Case eEAEI_BANESCO: enumEntidadAExportarOImportarToString = "Banesco"
      Case Else: enumEntidadAExportarOImportarToString = enumEntidadAExportarOImportarToString(0)
   End Select
End Function

Public Function strEntidadAExportarOImportarToNum(ByVal EntidadAExportarOImportar As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_EntidadAExportarOImportar
   Dim nCount As Integer
   nCount = 0
   For mEnum = eEAEI_PROVEEDOR To eEAEI_COTIZACION
   If UCase(EntidadAExportarOImportar) = UCase(enumEntidadAExportarOImportarToString(mEnum)) Then
      strEntidadAExportarOImportarToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strEntidadAExportarOImportarToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & EntidadAExportarOImportar & " no existe en tipo enumerativo EntidadAExportarOImportar"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumTipoDeAlicuotaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeAlicuota
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeAlicuota.eTD_ALICUOTA3 - enum_TipoDeAlicuota.eTD_EXENTO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeAlicuota.eTD_EXENTO To enum_TipoDeAlicuota.eTD_ALICUOTA3
   If valConSiglas = False Then
      varArray(nCount) = varCaracterDelimitador & enumTipoDeAlicuotaToString(varEnum) & varCaracterDelimitador
   Else
      varArray(nCount) = varCaracterDelimitador & enumTipoDeAlicuotaToStringSiglas(varEnum) & varCaracterDelimitador
   End If
      nCount = nCount + 1
   Next
   fEnumTipoDeAlicuotaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumFormaDeOrdenarDetalleFacturaToString(formaDeOrdenarDetalleFactura As enum_FormaDeOrdenarDetalleFactura) As String
   Select Case formaDeOrdenarDetalleFactura
      Case eFD_COMO_FUE_CARGADA: enumFormaDeOrdenarDetalleFacturaToString = "Como fue cargada"
      Case eFD_POR_CODIGO_DE_ARTICULO: enumFormaDeOrdenarDetalleFacturaToString = "Por Codigo de Artículo"
      Case eFD_POR_DESCRIPCION_DE_ARTICULO: enumFormaDeOrdenarDetalleFacturaToString = "Por Descripción de Artículo"
      Case Else
         enumFormaDeOrdenarDetalleFacturaToString = enumFormaDeOrdenarDetalleFacturaToString(0)
         End Select
End Function

Public Function strFormaDeOrdenarDetalleFacturaToNum(ByVal formaDeOrdenarDetalleFactura As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeOrdenarDetalleFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_COMO_FUE_CARGADA To eFD_POR_DESCRIPCION_DE_ARTICULO
      If UCase(formaDeOrdenarDetalleFactura) = UCase(enumFormaDeOrdenarDetalleFacturaToString(mEnum)) Then
         strFormaDeOrdenarDetalleFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeOrdenarDetalleFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeOrdenarDetalleFactura & " no existe en tipo enumerativo formaDeOrdenarDetalleFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeOrdenarDetalleFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumFormaDeOrdenarDetalleFacturaToString(enum_FormaDeOrdenarDetalleFactura.eFD_COMO_FUE_CARGADA)
   End If
   varComboBox.List(nCount) = enumFormaDeOrdenarDetalleFacturaToString(enum_FormaDeOrdenarDetalleFactura.eFD_COMO_FUE_CARGADA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumFormaDeOrdenarDetalleFacturaToString(enum_FormaDeOrdenarDetalleFactura.eFD_POR_CODIGO_DE_ARTICULO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumFormaDeOrdenarDetalleFacturaToString(enum_FormaDeOrdenarDetalleFactura.eFD_POR_DESCRIPCION_DE_ARTICULO)
   If action = Buscar Then
      varComboBox.ListIndex = 0
   Else
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumFormaDeOrdenarCodigosStringToString(formaDeOrdenarCodigosString As enum_FormaDeOrdenarCodigosString) As String
   Select Case formaDeOrdenarCodigosString
      Case eFD_NORMAL: enumFormaDeOrdenarCodigosStringToString = "Normal"
      Case eFD_ESPECIAL: enumFormaDeOrdenarCodigosStringToString = "Especial"
      Case Else
         enumFormaDeOrdenarCodigosStringToString = enumFormaDeOrdenarCodigosStringToString(0)
         End Select
End Function

Public Function strFormaDeOrdenarCodigosStringToNum(ByVal formaDeOrdenarCodigosString As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDeOrdenarCodigosString
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFD_NORMAL To eFD_ESPECIAL
      If UCase(formaDeOrdenarCodigosString) = UCase(enumFormaDeOrdenarCodigosStringToString(mEnum)) Then
         strFormaDeOrdenarCodigosStringToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFormaDeOrdenarCodigosStringToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formaDeOrdenarCodigosString & " no existe en tipo enumerativo formaDeOrdenarCodigosString"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFormaDeOrdenarCodigosString(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumFormaDeOrdenarCodigosStringToString(enum_FormaDeOrdenarCodigosString.eFD_NORMAL)
   End If
   varComboBox.List(nCount) = enumFormaDeOrdenarCodigosStringToString(enum_FormaDeOrdenarCodigosString.eFD_NORMAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumFormaDeOrdenarCodigosStringToString(enum_FormaDeOrdenarCodigosString.eFD_ESPECIAL)
   If action = Buscar Then
      varComboBox.ListIndex = 0
   Else
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumStatusAnticipoToString(statusAnticipo As enum_StatusAnticipo) As String
   Select Case statusAnticipo
      Case eSDA_VIGENTE: enumStatusAnticipoToString = "Vigente"
      Case eSDA_ANULADO: enumStatusAnticipoToString = "Anulado"
      Case eSDA_PARCIALMENTE_USADO: enumStatusAnticipoToString = "Parcialmente Usado"
      Case eSDA_COMPLETAMENTE_USADO: enumStatusAnticipoToString = "Completamente Usado"
      Case eSDA_COMPLETAMENTE_DEVUELTO: enumStatusAnticipoToString = "Completamente Devuelto"
      Case eSDA_PARCIALMENTE_DEVUELTO: enumStatusAnticipoToString = "Parcialmente Devuelto"
      Case Else: enumStatusAnticipoToString = enumStatusAnticipoToString(0)
   End Select
End Function

Public Function strStatusAnticipoToNum(ByVal statusAnticipo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusAnticipo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSDA_VIGENTE To eSDA_PARCIALMENTE_DEVUELTO
      If UCase(statusAnticipo) = UCase(enumStatusAnticipoToString(mEnum)) Then
         strStatusAnticipoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusAnticipoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusAnticipo & " no existe en tipo enumerativo statusAnticipo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusAnticipo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_VIGENTE)
   End If
   varComboBox.List(nCount) = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_VIGENTE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_ANULADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_COMPLETAMENTE_USADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_COMPLETAMENTE_DEVUELTO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusAnticipoToString(enum_StatusAnticipo.eSDA_PARCIALMENTE_DEVUELTO)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function fenumStatusAnticipoToStrinInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusAnticipo
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusAnticipo.eSDA_PARCIALMENTE_DEVUELTO - enum_StatusAnticipo.eSDA_VIGENTE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSDA_VIGENTE To eSDA_PARCIALMENTE_DEVUELTO
      varArray(nCount) = varCaracterDelimitador & enumStatusAnticipoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumStatusAnticipoToStrinInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeAnticipoToString(ByVal tipoDeAnticipo As enum_TipoDeAnticipo) As String
   Select Case tipoDeAnticipo
      Case eTDA_COBRADO: enumTipoDeAnticipoToString = "Cobrado"
      Case eTDA_PAGADO: enumTipoDeAnticipoToString = "Pagado"
      Case eTDA_ADELANTO: enumTipoDeAnticipoToString = "Adelanto"
      Case Else: enumTipoDeAnticipoToString = enumTipoDeAnticipoToString(0)
   End Select
End Function

Public Function strTipoDeAnticipoToNum(ByVal tipoDeAnticipo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeAnticipo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDA_COBRADO To eTDA_ADELANTO
      If UCase(tipoDeAnticipo) = UCase(enumTipoDeAnticipoToString(mEnum)) Then
         strTipoDeAnticipoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeAnticipoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeAnticipo & " no existe en tipo enumerativo tipoDeAnticipo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeAnticipo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeAnticipoToString(enum_TipoDeAnticipo.eTDA_COBRADO)
   End If
   varComboBox.List(nCount) = enumTipoDeAnticipoToString(enum_TipoDeAnticipo.eTDA_COBRADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeAnticipoToString(enum_TipoDeAnticipo.eTDA_PAGADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeAnticipoToString(enum_TipoDeAnticipo.eTDA_ADELANTO)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeFormaDePagoToString(tipoDeFormaDePago As enum_TipoDeFormaDePago) As String
   Select Case tipoDeFormaDePago
      Case eTDA_EFECTIVO: enumTipoDeFormaDePagoToString = "Efectivo"
      Case eTDA_TARJETA: enumTipoDeFormaDePagoToString = "Tarjeta"
      Case eTDA_CHEQUE: enumTipoDeFormaDePagoToString = "Cheque"
      Case eTDA_DEPOSITO: enumTipoDeFormaDePagoToString = "Depósito"
      Case eTDA_ANTICIPO: enumTipoDeFormaDePagoToString = "Anticipo"
      Case Else: enumTipoDeFormaDePagoToString = enumTipoDeFormaDePagoToString(0)
   End Select
End Function

Public Function strTipoDeFormaDePagoToNum(ByVal tipoDeFormaDePago As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeFormaDePago
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDA_EFECTIVO To eTDA_ANTICIPO
      If UCase(tipoDeFormaDePago) = UCase(enumTipoDeFormaDePagoToString(mEnum)) Then
         strTipoDeFormaDePagoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeFormaDePagoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeFormaDePago & " no existe en tipo enumerativo TipoDeFormaDePago"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeFormaDePago(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeFormaDePagoToString(enum_TipoDeFormaDePago.eTDA_EFECTIVO)
   End If
   varComboBox.List(nCount) = enumTipoDeFormaDePagoToString(enum_TipoDeFormaDePago.eTDA_EFECTIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeFormaDePagoToString(enum_TipoDeFormaDePago.eTDA_TARJETA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeFormaDePagoToString(enum_TipoDeFormaDePago.eTDA_CHEQUE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeFormaDePagoToString(enum_TipoDeFormaDePago.eTDA_DEPOSITO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeFormaDePagoToString(enum_TipoDeFormaDePago.eTDA_ANTICIPO)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumPuertoSerialToString(PuertoSerial As enum_PuertoSerial) As String
   Select Case PuertoSerial
      Case ePS_COM1: enumPuertoSerialToString = "COM1"
      Case ePS_COM2: enumPuertoSerialToString = "COM2"
      Case ePS_COM3: enumPuertoSerialToString = "COM3"
      Case ePS_COM4: enumPuertoSerialToString = "COM4"
      Case ePS_COM5: enumPuertoSerialToString = "COM5"
      Case ePS_COM6: enumPuertoSerialToString = "COM6"
      Case ePS_COM7: enumPuertoSerialToString = "COM7"
      Case ePS_COM8: enumPuertoSerialToString = "COM8"
      Case ePS_COM9: enumPuertoSerialToString = "COM9"
      Case ePS_COM10: enumPuertoSerialToString = "COM10"
      Case ePS_COM11: enumPuertoSerialToString = "COM11"
      Case ePS_COM12: enumPuertoSerialToString = "COM12"
      Case ePS_COM13: enumPuertoSerialToString = "COM13"
      Case ePS_COM14: enumPuertoSerialToString = "COM14"
      Case ePS_COM15: enumPuertoSerialToString = "COM15"
      Case ePS_COM16: enumPuertoSerialToString = "COM16"
      Case Else
         enumPuertoSerialToString = enumPuertoSerialToString(0)
   End Select
End Function

Public Function strPuertoSerialToNum(ByVal PuertoSerial As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_PuertoSerial
   Dim nCount As Integer
   nCount = 1
   For mEnum = ePS_COM1 To ePS_COM16
      If UCase(PuertoSerial) = UCase(enumPuertoSerialToString(mEnum)) Then
         strPuertoSerialToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strPuertoSerialToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & PuertoSerial & " no existe en tipo enumerativo PuertoSerial"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithPuertoSerial(ByRef varComboBox As Object, ByVal varMaximo As Integer, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM1)
   End If
   If (nCount < varMaximo) Then
    varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM1)
    nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM2)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM3)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM4)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM5)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM6)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM7)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM8)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM9)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM10)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM11)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM12)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM13)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM14)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM15)
   nCount = nCount + 1
   End If
   If (nCount < varMaximo) Then
   varComboBox.List(nCount) = enumPuertoSerialToString(enum_PuertoSerial.ePS_COM16)
   End If
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox) + 10
End Sub

Public Function enumTipoDeNivelDePreciosToString(TipoDeNivelDePrecios As enum_TipoDeNivelDePrecios) As String
   Select Case TipoDeNivelDePrecios
      Case eTDNDP_PorUsuario: enumTipoDeNivelDePreciosToString = "Por Usuario"
      Case eTDNDP_PorCliente: enumTipoDeNivelDePreciosToString = "Por Cliente"
      Case Else
         enumTipoDeNivelDePreciosToString = enumTipoDeNivelDePreciosToString(0)
   End Select
End Function


Public Function strTipoDeNivelDePreciosToNum(ByVal TipoDeNivelDePrecios As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeNivelDePrecios
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDNDP_PorUsuario To eTDNDP_PorCliente
      If UCase(TipoDeNivelDePrecios) = UCase(enumTipoDeNivelDePreciosToString(mEnum)) Then
         strTipoDeNivelDePreciosToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeNivelDePreciosToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeNivelDePrecios & " no existe en tipo enumerativo TipoDeNivelDePrecios"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeNivelDePrecios(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeNivelDePreciosToString(enum_TipoDeNivelDePrecios.eTDNDP_PorUsuario)
   End If
   varComboBox.List(nCount) = enumTipoDeNivelDePreciosToString(enum_TipoDeNivelDePrecios.eTDNDP_PorUsuario)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeNivelDePreciosToString(enum_TipoDeNivelDePrecios.eTDNDP_PorCliente)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumComprobanteConChequeToString(TipoComprobanteChequeAImprimir As enum_ComprobanteConCheque) As String
   Select Case TipoComprobanteChequeAImprimir
      Case eCC_ComprobanteConCheque: enumComprobanteConChequeToString = "Comprobante con  Cheque"
      Case eCC_SoloComprobante: enumComprobanteConChequeToString = "Sólo Comprobante"
      Case eCC_ComprobanteSeparadoDeCheque: enumComprobanteConChequeToString = "Comprobante separado de Cheque"
      Case Else: enumComprobanteConChequeToString = enumComprobanteConChequeToString(0)
   End Select
End Function

Public Function strComprobanteConChequeToNum(ByVal valComprobanteCheque As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ComprobanteConCheque
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCC_ComprobanteConCheque To eCC_ComprobanteSeparadoDeCheque
      If UCase(valComprobanteCheque) = UCase(enumComprobanteConChequeToString(mEnum)) Then
         strComprobanteConChequeToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strComprobanteConChequeToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valComprobanteCheque & " no existe en tipo enumerativo valComprobanteCheque"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithComprobanteCheque(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComprobanteConChequeToString(enum_ComprobanteConCheque.eCC_ComprobanteConCheque)
   End If
   varComboBox.List(nCount) = enumComprobanteConChequeToString(enum_ComprobanteConCheque.eCC_ComprobanteConCheque)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteConChequeToString(enum_ComprobanteConCheque.eCC_SoloComprobante)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComprobanteConChequeToString(enum_ComprobanteConCheque.eCC_ComprobanteSeparadoDeCheque)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function fCodigoConceptoBancarioPorDefecto(ByVal valCualConcepto As enum_ConceptoBancarioPorDefecto) As String
   Select Case valCualConcepto
      Case eCB_DEBITO_BANCARIO_AUTOMATICO: fCodigoConceptoBancarioPorDefecto = "60335" '"DEBITO BANCARIO AUTOMATICO"
      Case eCB_REVERSO_AUTOMATICO_COBRANZA: fCodigoConceptoBancarioPorDefecto = "60334" '"REVERSO AUTOMATICO COBRANZA"
      Case eCB_REV_AUTOMATICO_ANT_COBRADO: fCodigoConceptoBancarioPorDefecto = "60336" '"REV. AUTOMATICO ANT. COBRADO"
      Case eCB_REV_AUTOMATICO_ANT_PAGADO: fCodigoConceptoBancarioPorDefecto = "60337" '"REV. AUTOMATICO ANT. PAGADO"
      Case eCB_ANTICIPO_COBRADO: fCodigoConceptoBancarioPorDefecto = "60338" '"ANTICIPO COBRADO"
      Case eCB_ANTICIPO_PAGADO: fCodigoConceptoBancarioPorDefecto = "60339" '"ANTICIPO PAGADO"
      Case eCB_COBRO_DIRECTO_DE_FACT: fCodigoConceptoBancarioPorDefecto = "60340" '"COBRO DIRECTO DE FACT."
      Case eCB_REVERSO_AUTOMATICO_PAGO: fCodigoConceptoBancarioPorDefecto = "60341" '"REVERSO AUTOMATICO PAGO"
      Case eCB_TRANSERENCIA_INGRESO: fCodigoConceptoBancarioPorDefecto = "60342" '"INGRESO POR TRANSFERENCIA"
      Case eCB_TRANSERENCIA_EGRESO: fCodigoConceptoBancarioPorDefecto = "60343" '"EGRESO POR TRANSFERENCIA"
      Case eCB_DETRACCION_INGRESO: fCodigoConceptoBancarioPorDefecto = "60344" '"INGRESO POR DETRACCION"
      Case eCB_DETRACCION_EGRESO: fCodigoConceptoBancarioPorDefecto = "60345" '"EGRESO POR DETRACCION"
      Case eCB_REV_AUTOMATICO_SOLI_PAGADO: fCodigoConceptoBancarioPorDefecto = "G0001"  '"REVERSO AUTOMATICO SOLICITUD DE PAGO"
      Case eCB_PAGO_RETENCION_DIRECTO_DESDE_CXP: fCodigoConceptoBancarioPorDefecto = "60346" '"PAGO DIRECTO RETENCION DESDE CXP"
      Case eCB_PAGO_CAJA_CHICA: fCodigoConceptoBancarioPorDefecto = "CC001" '"PAGO A PROVEEDORES DESDE CAJA CHICA
      Case eCB_EGRESO_REPOSICION_CAJA_CHICA: fCodigoConceptoBancarioPorDefecto = "CC002" '"EGRESO DESDE X CUENTA BANCARIA HACIA CAJA CHICA
      Case eCB_INGRESO_REPOSICION_CAJA_CHICA: fCodigoConceptoBancarioPorDefecto = "CC003" '"INGRESO PARA REPONER EL MONTO DE CAJA CHICA
      Case eCB_REVERSO_PAGO_CAJA_CHICA: fCodigoConceptoBancarioPorDefecto = "CC004" '"REVERSO PAGO A PROVEEDORES DESDE CAJA CHICA
      Case eCB_REVERSO_EGRESO_REPOSICION_CAJA_CHICA: fCodigoConceptoBancarioPorDefecto = "CC005" '"REVERSO EGRESO DESDE X CUENTA BANCARIA HACIA CAJA CHICA
      Case eCB_REVERSO_INGRESO_REPOSICION_CAJA_CHICA: fCodigoConceptoBancarioPorDefecto = "CC006" '"REVERSO INGRESO PARA REPONER EL MONTO DE CAJA CHICA

   End Select
End Function

Public Function fDescripcionConceptoBancarioPorDefecto(ByVal valCualConcepto As enum_ConceptoBancarioPorDefecto) As String
   Select Case valCualConcepto
      Case eCB_DEBITO_BANCARIO_AUTOMATICO: fDescripcionConceptoBancarioPorDefecto = "DEBITO BANCARIO AUTOMATICO" '"60335"
      Case eCB_REVERSO_AUTOMATICO_COBRANZA: fDescripcionConceptoBancarioPorDefecto = "REVERSO AUTOMATICO COBRANZA"  '"60334"
      Case eCB_REV_AUTOMATICO_ANT_COBRADO: fDescripcionConceptoBancarioPorDefecto = "REV. AUTOMATICO ANT. COBRADO"  '"60336"
      Case eCB_REV_AUTOMATICO_ANT_PAGADO: fDescripcionConceptoBancarioPorDefecto = "REV. AUTOMATICO ANT. PAGADO" '"60337"
      Case eCB_ANTICIPO_COBRADO: fDescripcionConceptoBancarioPorDefecto = "ANTICIPO COBRADO" '"60338"
      Case eCB_ANTICIPO_PAGADO: fDescripcionConceptoBancarioPorDefecto = "ANTICIPO PAGADO" '"60339"
      Case eCB_COBRO_DIRECTO_DE_FACT: fDescripcionConceptoBancarioPorDefecto = "COBRO DIRECTO DE FACT." '"60340"
      Case eCB_REVERSO_AUTOMATICO_PAGO: fDescripcionConceptoBancarioPorDefecto = "REVERSO AUTOMATICO PAGO" '"60341"
      Case eCB_TRANSERENCIA_INGRESO: fDescripcionConceptoBancarioPorDefecto = "INGRESO POR TRANSFERENCIA" '"60342"
      Case eCB_TRANSERENCIA_EGRESO: fDescripcionConceptoBancarioPorDefecto = "EGRESO POR TRANSFERENCIA" '"60343"
      Case eCB_DETRACCION_INGRESO: fDescripcionConceptoBancarioPorDefecto = "INGRESO POR DETRACCION" '"60344"
      Case eCB_DETRACCION_EGRESO: fDescripcionConceptoBancarioPorDefecto = "EGRESO POR DETRACCION" '"60345"
      Case eCB_REV_AUTOMATICO_SOLI_PAGADO: fDescripcionConceptoBancarioPorDefecto = "REVERSO AUTOMATICO SOLICITUD DE PAGO"  '"G0001"'
      Case eCB_PAGO_RETENCION_DIRECTO_DESDE_CXP: fDescripcionConceptoBancarioPorDefecto = "PAGO DIRECTO RETENCION DESDE CXP." '"60346"
      Case eCB_PAGO_CAJA_CHICA: fDescripcionConceptoBancarioPorDefecto = "PAGO PROVEEDORES - CAJA CHICA" '"CC001
      Case eCB_EGRESO_REPOSICION_CAJA_CHICA: fDescripcionConceptoBancarioPorDefecto = "EGRESO - REPOSICION CAJA CHICA" '"CC002
      Case eCB_INGRESO_REPOSICION_CAJA_CHICA: fDescripcionConceptoBancarioPorDefecto = "REPOSICION CAJA CHICA" '"CC003
      Case eCB_REVERSO_PAGO_CAJA_CHICA: fDescripcionConceptoBancarioPorDefecto = "REV PAGO PROVEEDORES-C.CHICA" '"CC004
      Case eCB_REVERSO_EGRESO_REPOSICION_CAJA_CHICA: fDescripcionConceptoBancarioPorDefecto = "REV EGRESO-REPOSICION C.CHICA" '"CC005
      Case eCB_REVERSO_INGRESO_REPOSICION_CAJA_CHICA: fDescripcionConceptoBancarioPorDefecto = "REV REPOSICION CAJA CHICA" '"CC006
   End Select
End Function

Public Function fTipoDeConceptoBancarioPorDefecto(ByVal valCualConcepto As enum_ConceptoBancarioPorDefecto) As enum_IngresoEgreso
   Select Case valCualConcepto
      Case eCB_DEBITO_BANCARIO_AUTOMATICO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"60335"
      Case eCB_REVERSO_AUTOMATICO_COBRANZA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO  '"60334"
      Case eCB_REV_AUTOMATICO_ANT_COBRADO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO  '"60336"
      Case eCB_REV_AUTOMATICO_ANT_PAGADO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"60337"
      Case eCB_ANTICIPO_COBRADO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"60338"
      Case eCB_ANTICIPO_PAGADO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"60339"
      Case eCB_COBRO_DIRECTO_DE_FACT: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"60340"
      Case eCB_REVERSO_AUTOMATICO_PAGO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"60341"
      Case eCB_DETRACCION_INGRESO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"60341"
      Case eCB_DETRACCION_EGRESO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"60342"
      Case eCB_TRANSERENCIA_INGRESO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"60343"
      Case eCB_TRANSERENCIA_EGRESO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"60343"
      Case eCB_REV_AUTOMATICO_SOLI_PAGADO: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"G0001"'
      Case eCB_PAGO_RETENCION_DIRECTO_DESDE_CXP: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"60346"
      Case eCB_PAGO_CAJA_CHICA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"CC001
      Case eCB_EGRESO_REPOSICION_CAJA_CHICA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"CC002
      Case eCB_INGRESO_REPOSICION_CAJA_CHICA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"CC003
      Case eCB_REVERSO_PAGO_CAJA_CHICA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"CC004
      Case eCB_REVERSO_EGRESO_REPOSICION_CAJA_CHICA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_INGRESO '"CC005
      Case eCB_REVERSO_INGRESO_REPOSICION_CAJA_CHICA: fTipoDeConceptoBancarioPorDefecto = enum_IngresoEgreso.eIE_EGRESO '"CC006
   End Select
End Function

Public Function fEnumCreditoFiscalToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_CreditoFiscal
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_CreditoFiscal.eCF_NoDeducible - enum_CreditoFiscal.eCF_DEDUCIBLE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_CreditoFiscal.eCF_DEDUCIBLE To enum_CreditoFiscal.eCF_NoDeducible
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumCreditoFiscalToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumCreditoFiscalToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumCreditoFiscalToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumStatusDelVendedorToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusVendedor
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusVendedor.eSV_RESTRINGIDO - enum_StatusVendedor.eSV_ACTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusVendedor.eSV_ACTIVO To enum_StatusVendedor.eSV_RESTRINGIDO
      varArray(nCount) = varCaracterDelimitador & enumStatusVendedorToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusDelVendedorToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumOrigenFacturacionOManualToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_OrigenFacturacionOManual
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_OrigenFacturacionOManual.eOF_MANUAL - enum_OrigenFacturacionOManual.eOF_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_OrigenFacturacionOManual.eOF_FACTURA To enum_OrigenFacturacionOManual.eOF_MANUAL
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumOrigenFacturacionOManualToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumOrigenFacturacionOManualToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumOrigenFacturacionOManualToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumDondeSeEfectuaLaRetencionIVAToStringInArray(ByVal valConSiglas As Boolean, Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_DondeSeEfectuaLaRetencionIVA
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_DondeSeEfectuaLaRetencionIVA.eDS_PAGO - enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_DondeSeEfectuaLaRetencionIVA.eDS_NO_RETENIDA To enum_DondeSeEfectuaLaRetencionIVA.eDS_PAGO
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumDondeSeEfectuaLaRetencionIVAToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumDondeSeEfectuaLaRetencionIVAToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumDondeSeEfectuaLaRetencionIVAToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeNivelDePreciosToStringSiglas(TipoDeNivelDePrecios As enum_TipoDeNivelDePrecios) As String
   Select Case TipoDeNivelDePrecios
      Case eTDNDP_PorUsuario: enumTipoDeNivelDePreciosToStringSiglas = "NUS"
      Case eTDNDP_PorCliente: enumTipoDeNivelDePreciosToStringSiglas = "NCL"
      Case Else:  enumTipoDeNivelDePreciosToStringSiglas = enumTipoDeNivelDePreciosToStringSiglas(0)
   End Select
End Function

Public Function enumStatusFacturaToStringSiglas(ByVal statusFactura As enum_StatusFactura) As String
   Select Case statusFactura
      Case eSF_EMITIDA: enumStatusFacturaToStringSiglas = "EMI"
      Case eSF_ANULADA: enumStatusFacturaToStringSiglas = "ANU"
      Case eSF_BORRADOR: enumStatusFacturaToStringSiglas = "BOR"
      Case Else: enumStatusFacturaToStringSiglas = enumStatusFacturaToStringSiglas(0)
   End Select
End Function

Public Function enumTalonarioToStringSiglas(ByVal talonario As enum_Talonario) As String
   Select Case talonario
      Case eTa_TALONARIO1: enumTalonarioToStringSiglas = "TN1"
      Case eTa_TALONARIO2: enumTalonarioToStringSiglas = "TN2"
      Case Else: enumTalonarioToStringSiglas = enumTalonarioToStringSiglas(0)
   End Select
End Function

Public Function enumFormaDeLaInicialToStringSiglas(ByVal formaDeLaInicial As enum_FormaDeLaInicial) As String
   Select Case formaDeLaInicial
      Case eFD_POR_MONTO: enumFormaDeLaInicialToStringSiglas = "MON"
      Case eFD_POR_PORCENTAJE: enumFormaDeLaInicialToStringSiglas = "POR"
      Case Else: enumFormaDeLaInicialToStringSiglas = enumFormaDeLaInicialToStringSiglas(0)
   End Select
End Function

Public Function enumTipoDeFormaDePagoToStringSiglas(tipoDeFormaDePago As enum_TipoDeFormaDePago) As String
   Select Case tipoDeFormaDePago
      Case eTDA_EFECTIVO: enumTipoDeFormaDePagoToStringSiglas = "EFE"
      Case eTDA_TARJETA: enumTipoDeFormaDePagoToStringSiglas = "TAR"
      Case eTDA_CHEQUE: enumTipoDeFormaDePagoToStringSiglas = "CHE"
      Case eTDA_DEPOSITO: enumTipoDeFormaDePagoToStringSiglas = "DEP"
      Case eTDA_ANTICIPO: enumTipoDeFormaDePagoToStringSiglas = "ANT"
      Case Else: enumTipoDeFormaDePagoToStringSiglas = enumTipoDeFormaDePagoToStringSiglas(0)
   End Select
End Function

Public Function enumFormadePagoToStringSiglas(ByVal formadePago As enum_FormaDePago) As String
   Select Case formadePago
      Case eFP_CONTADO: enumFormadePagoToStringSiglas = "CON"
      Case eFP_CREDITO: enumFormadePagoToStringSiglas = "CRE"
      Case Else: enumFormadePagoToStringSiglas = enumFormadePagoToStringSiglas(0)
   End Select
End Function

Public Function enumNivelDePrecioToStringSiglas(ByVal nivelDePrecio As enum_NivelDePrecio) As String
   Select Case nivelDePrecio
      Case eND_PRECIO_1: enumNivelDePrecioToStringSiglas = "PR1"
      Case eND_PRECIO_2: enumNivelDePrecioToStringSiglas = "PR2"
      Case eND_PRECIO_3: enumNivelDePrecioToStringSiglas = "PR3"
      Case eND_PRECIO_4: enumNivelDePrecioToStringSiglas = "PR4"
      Case Else: enumNivelDePrecioToStringSiglas = enumNivelDePrecioToStringSiglas(0)
   End Select
End Function

Public Function fSiglasTipoDeNivelDePreciosToEnum(ByVal TipoDeNivelDePrecios As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeNivelDePrecios
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDNDP_PorUsuario To eTDNDP_PorCliente
   If UCase(TipoDeNivelDePrecios) = UCase(enumTipoDeNivelDePreciosToStringSiglas(mEnum)) Then
      fSiglasTipoDeNivelDePreciosToEnum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   fSiglasTipoDeNivelDePreciosToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeNivelDePrecios & " no existe en tipo enumerativo statusVendedor"
   End If
h_EXIT:
   Exit Function

End Function

Public Function fSiglasNivelDePrecioToEnum(ByVal nivelDePrecio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_NivelDePrecio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eND_PRECIO_1 To eND_PRECIO_4
      If UCase(nivelDePrecio) = UCase(enumNivelDePrecioToStringSiglas(mEnum)) Then
         fSiglasNivelDePrecioToEnum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   fSiglasNivelDePrecioToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & nivelDePrecio & " no existe en tipo enumerativo nivelDePrecio"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fSiglasStatusFacturaToEnum(ByVal statusFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSF_EMITIDA To eSF_BORRADOR
   If UCase(statusFactura) = UCase(enumStatusFacturaToStringSiglas(mEnum)) Then
      fSiglasStatusFacturaToEnum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   fSiglasStatusFacturaToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusFactura & " no existe en tipo enumerativo statusFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fSiglasTalonarioToEnum(ByVal talonario As String) As Variant
   Select Case talonario
      Case "TN1": fSiglasTalonarioToEnum = enum_Talonario.eTa_TALONARIO1
      Case "TN2": fSiglasTalonarioToEnum = enum_Talonario.eTa_TALONARIO2
      Case Else: fSiglasTalonarioToEnum = talonario
   End Select
End Function

Public Function fSiglasFormaDeLaInicialToEnum(ByVal formaDeLaInicial As String) As Variant
   Select Case formaDeLaInicial
      Case "MON": fSiglasFormaDeLaInicialToEnum = enum_FormaDeLaInicial.eFD_POR_MONTO
      Case "POR": fSiglasFormaDeLaInicialToEnum = enum_FormaDeLaInicial.eFD_POR_PORCENTAJE
      Case Else: fSiglasFormaDeLaInicialToEnum = formaDeLaInicial
   End Select
End Function

Public Function fSiglasTipoDeFormaDePagoToEnum(ByVal tipoDeFormaDePago As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeFormaDePago
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDA_EFECTIVO To eTDA_ANTICIPO
      If UCase(tipoDeFormaDePago) = UCase(enumTipoDeFormaDePagoToStringSiglas(mEnum)) Then
         fSiglasTipoDeFormaDePagoToEnum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   fSiglasTipoDeFormaDePagoToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeFormaDePago & " no existe en tipo enumerativo TipoDeFormaDePago"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fSiglasFormadePagoToEnum(ByVal formadePago As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FormaDePago
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFP_CONTADO To eFP_CREDITO
   If UCase(formadePago) = UCase(enumFormadePagoToStringSiglas(mEnum)) Then
      fSiglasFormadePagoToEnum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   fSiglasFormadePagoToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & formadePago & " no existe en tipo enumerativo formadePago"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fSiglasTipoDeTransaccionDeLibrosFiscalesToEnum(ByVal tipoDeTransaccionDeLibrosFiscales As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeTransaccionDeLibrosFiscales
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_A01_REGISTRO To eTD_A04_AJUSTE
      If UCase(tipoDeTransaccionDeLibrosFiscales) = UCase(enumTipoDeTransaccionDeLibrosFiscalesToSiglas(mEnum)) Then
         fSiglasTipoDeTransaccionDeLibrosFiscalesToEnum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   fSiglasTipoDeTransaccionDeLibrosFiscalesToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeTransaccionDeLibrosFiscales & " no existe en tipo enumerativo tipoDeTransaccionDeLibrosFiscales"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fSiglasTipoDeVentaToEnum(ByVal tipoDeVenta As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeVenta
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_INTERNA To eTD_A_NO_CONTRIBUYENTE
      If UCase(tipoDeVenta) = UCase(enumTipoDeVentaToStringSiglas(mEnum)) Then
         fSiglasTipoDeVentaToEnum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   fSiglasTipoDeVentaToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeVenta & " no existe en tipo enumerativo tipoDeVenta"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fSiglasTipoDocumentoFacturaToEnum(ByVal tipodocumentoFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDocumentoFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTF_FACTURA To eTF_NOTAENTREGA
   If UCase(tipodocumentoFactura) = UCase(enumTipoDocumentoFacturaToSiglas(mEnum)) Then
      fSiglasTipoDocumentoFacturaToEnum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   fSiglasTipoDocumentoFacturaToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipodocumentoFactura & " no existe en tipo enumerativo tipodocumentoFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumNivelDePrecioToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_NivelDePrecio
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_NivelDePrecio.eND_PRECIO_4 - enum_NivelDePrecio.eND_PRECIO_1
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_NivelDePrecio.eND_PRECIO_1 To enum_NivelDePrecio.eND_PRECIO_4
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumNivelDePrecioToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumNivelDePrecioToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumNivelDePrecioToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumStatusFacturaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_StatusFactura
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusFactura.eSF_BORRADOR - enum_StatusFactura.eSF_EMITIDA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusFactura.eSF_EMITIDA To enum_StatusFactura.eSF_BORRADOR
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumStatusFacturaToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumStatusFacturaToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumStatusFacturaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumTalonarioToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_Talonario
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_Talonario.eTa_TALONARIO2 - enum_Talonario.eTa_TALONARIO1
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_Talonario.eTa_TALONARIO1 To enum_Talonario.eTa_TALONARIO2
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTalonarioToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTalonarioToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumTalonarioToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumFormaDeLaInicialToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_FormaDeLaInicial
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_FormaDeLaInicial.eFD_POR_PORCENTAJE - enum_FormaDeLaInicial.eFD_POR_MONTO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_FormaDeLaInicial.eFD_POR_MONTO To enum_FormaDeLaInicial.eFD_POR_PORCENTAJE
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumFormaDeLaInicialToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumFormaDeLaInicialToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumFormaDeLaInicialToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumFormaDePagoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_FormaDePago
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_FormaDePago.eFP_CREDITO - enum_FormaDePago.eFP_CONTADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_FormaDePago.eFP_CONTADO To enum_FormaDePago.eFP_CREDITO
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumFormadePagoToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumFormadePagoToStringSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumFormaDePagoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDeAlicuotaToStringSiglas(ByVal TipoDeAlicuota As enum_TipoDeAlicuota) As String
   Select Case TipoDeAlicuota
      Case eTD_EXENTO: enumTipoDeAlicuotaToStringSiglas = "ETO"
      Case eTD_ALICUOTAGENERAL: enumTipoDeAlicuotaToStringSiglas = "ALG"
      Case eTD_ALICUOTA2: enumTipoDeAlicuotaToStringSiglas = "ALR"
      Case eTD_ALICUOTA3: enumTipoDeAlicuotaToStringSiglas = "ALA"
      Case Else: enumTipoDeAlicuotaToStringSiglas = enumTipoDeAlicuotaToStringSiglas(0)
   End Select
End Function

Public Function fSiglasTipoDeAlicuotaToEnum(ByVal TipoDeAlicuota As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeAlicuota
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_EXENTO To eTD_ALICUOTA3
   If UCase(TipoDeAlicuota) = UCase(enumTipoDeAlicuotaToStringSiglas(mEnum)) Then
      fSiglasTipoDeAlicuotaToEnum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   fSiglasTipoDeAlicuotaToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeAlicuota & " no existe en tipo enumerativo tipoDeAlicuota"
   End If
h_EXIT:
   Exit Function
End Function
Public Function enumMesesDelAnoToString(ByVal valMesesDelAno As enum_MesesDelAno) As String
   Select Case valMesesDelAno
      Case eMDA_ENERO: enumMesesDelAnoToString = "Enero"
      Case eMDA_FEBRERO: enumMesesDelAnoToString = "Febrero"
      Case eMDA_MARZO: enumMesesDelAnoToString = "Marzo"
      Case eMDA_ABRIL: enumMesesDelAnoToString = "Abril"
      Case eMDA_MAYO: enumMesesDelAnoToString = "Mayo"
      Case eMDA_JUNIO: enumMesesDelAnoToString = "Junio"
      Case eMDA_JULIO: enumMesesDelAnoToString = "Julio"
      Case eMDA_AGOSTO: enumMesesDelAnoToString = "Agosto"
      Case eMDA_SEPTIEMBRE: enumMesesDelAnoToString = "Septiembre"
      Case eMDA_OCTUBRE: enumMesesDelAnoToString = "Octubre"
      Case eMDA_NOVIEMBRE: enumMesesDelAnoToString = "Noviembre"
      Case eMDA_DICIEMBRE: enumMesesDelAnoToString = "Diciembre"
      Case Else: enumMesesDelAnoToString = enumMesesDelAnoToString(1)
   End Select
End Function

Public Function strMesesDelAnoToNum(ByVal valMesesDelAno As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MesesDelAno
   Dim nCount As Integer
   nCount = 1
   For mEnum = eMDA_ENERO To eMDA_DICIEMBRE
      If UCase(valMesesDelAno) = UCase(enumMesesDelAnoToString(mEnum)) Then
         strMesesDelAnoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMesesDelAnoToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valMesesDelAno & " no existe en tipo enumerativo valComprobanteCheque"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithMesesDelAno(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_ENERO)
   End If
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_ENERO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_FEBRERO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_MARZO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_ABRIL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_MAYO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_JUNIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_JULIO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_AGOSTO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_SEPTIEMBRE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_OCTUBRE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_NOVIEMBRE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMesesDelAnoToString(enum_MesesDelAno.eMDA_DICIEMBRE)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumTipoComisionToString(TipoComision As enum_TipoComision) As String
   Select Case TipoComision
      Case eTC_PorPorcentaje: enumTipoComisionToString = "Por  Porcentaje"
      Case eTC_PorMonto: enumTipoComisionToString = "Por  Monto"
      Case Else
         enumTipoComisionToString = enumTipoComisionToString(0)
   End Select
End Function

Public Function strTipoComisionToNum(ByVal TipoComision As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoComision
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTC_PorPorcentaje To eTC_PorMonto
      If UCase(TipoComision) = UCase(enumTipoComisionToString(mEnum)) Then
         strTipoComisionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoComisionToNum = eTC_PorPorcentaje
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoComision & " no existe en tipo enumerativo TipoComision"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoComision(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoComisionToString(enum_TipoComision.eTC_PorPorcentaje)
   End If
   varComboBox.List(nCount) = enumTipoComisionToString(enum_TipoComision.eTC_PorPorcentaje)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoComisionToString(enum_TipoComision.eTC_PorMonto)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumComisionesEnFacturaToString(ComisionesEnFactura As enum_ComisionesEnFactura) As String
   Select Case ComisionesEnFactura
      Case eCEF_SobreTotalFactura: enumComisionesEnFacturaToString = "Sobre Total Factura"
      Case eCEF_SobreRenglones: enumComisionesEnFacturaToString = "Sobre Renglones"
      Case eCEF_SobreTotalFacturayRenglones: enumComisionesEnFacturaToString = "Sobre Total Factura y Renglones"
      Case Else
         enumComisionesEnFacturaToString = enumComisionesEnFacturaToString(0)
   End Select
End Function

Public Function strComisionesEnFacturaToNum(ByVal ComisionesEnFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ComisionesEnFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCEF_SobreTotalFactura To eCEF_SobreTotalFacturayRenglones
      If UCase(ComisionesEnFactura) = UCase(enumComisionesEnFacturaToString(mEnum)) Then
         strComisionesEnFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strComisionesEnFacturaToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ComisionesEnFactura & " no existe en tipo enumerativo ComisionesEnFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithComisionesEnFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComisionesEnFacturaToString(enum_ComisionesEnFactura.eCEF_SobreTotalFactura)
   End If
   varComboBox.List(nCount) = enumComisionesEnFacturaToString(enum_ComisionesEnFactura.eCEF_SobreTotalFactura)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComisionesEnFacturaToString(enum_ComisionesEnFactura.eCEF_SobreRenglones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComisionesEnFacturaToString(enum_ComisionesEnFactura.eCEF_SobreTotalFacturayRenglones)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumComisionesEnRenglonesToString(ComisionesEnRenglones As enum_ComisionesEnRenglones) As String
   Select Case ComisionesEnRenglones
      Case eCER_PorUnVendedor: enumComisionesEnRenglonesToString = "Por Un Vendedor"
      Case eCER_PordosVendedores: enumComisionesEnRenglonesToString = "Por Dos Vendedores"
      Case eCER_PorTresVendedores: enumComisionesEnRenglonesToString = "Por Tres Vendedores"
      Case Else
         enumComisionesEnRenglonesToString = enumComisionesEnRenglonesToString(0)
   End Select
End Function

Public Function strComisionesEnRenglonesToNum(ByVal ComisionesEnRenglones As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ComisionesEnRenglones
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCER_PorUnVendedor To eCER_PorTresVendedores
      If UCase(ComisionesEnRenglones) = UCase(enumComisionesEnRenglonesToString(mEnum)) Then
         strComisionesEnRenglonesToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strComisionesEnRenglonesToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ComisionesEnRenglones & " no existe en tipo enumerativo ComisionesEnRenglones"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithComisionesEnRenglones(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumComisionesEnRenglonesToString(enum_ComisionesEnRenglones.eCER_PorUnVendedor)
   End If
   varComboBox.List(nCount) = enumComisionesEnRenglonesToString(enum_ComisionesEnRenglones.eCER_PorUnVendedor)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComisionesEnRenglonesToString(enum_ComisionesEnRenglones.eCER_PordosVendedores)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumComisionesEnRenglonesToString(enum_ComisionesEnRenglones.eCER_PorTresVendedores)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function fenumTipoFormaDePagoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeFormaDePago
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeFormaDePago.eTDA_ANTICIPO - enum_TipoDeFormaDePago.eTDA_EFECTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTDA_EFECTIVO To eTDA_ANTICIPO
      varArray(nCount) = varCaracterDelimitador & enumTipoDeFormaDePagoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTipoFormaDePagoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function strItemsMontoToNum(ByVal ItemsMonto As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ItemsMonto
   Dim nCount As Integer
   nCount = 0
   For mEnum = eIM_NO_PERMITIR_ITEMS_NEGATIVOS To eIM_PERMITIR_SIEMPRE
      If UCase(ItemsMonto) = UCase(enumItemsMontoFacturaToString(mEnum)) Then
         strItemsMontoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strItemsMontoToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ItemsMonto & " no existe en tipo enumerativo ComisionesEnRenglones"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithItemsMontoFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumItemsMontoFacturaToString(enum_ItemsMonto.eIM_NO_PERMITIR_ITEMS_NEGATIVOS)
   End If
   varComboBox.List(nCount) = enumItemsMontoFacturaToString(enum_ItemsMonto.eIM_NO_PERMITIR_ITEMS_NEGATIVOS)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumItemsMontoFacturaToString(enum_ItemsMonto.eIM_PERMITIR_CON_AUTORIZACION)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumItemsMontoFacturaToString(enum_ItemsMonto.eIM_PERMITIR_SIEMPRE)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumItemsMontoFacturaToString(ComisionesEnRenglones As enum_ComisionesEnRenglones) As String
   Select Case ComisionesEnRenglones
      Case eIM_NO_PERMITIR_ITEMS_NEGATIVOS: enumItemsMontoFacturaToString = "No Permitir Items Negativos"
      Case eIM_PERMITIR_CON_AUTORIZACION: enumItemsMontoFacturaToString = "Permitir con Autorización"
      Case eIM_PERMITIR_SIEMPRE: enumItemsMontoFacturaToString = "Permitir Siempre"
      Case Else
         enumItemsMontoFacturaToString = enumItemsMontoFacturaToString(0)
   End Select
End Function
Public Function enumStatusVendedorToStringSiglas(ByVal statusVendedor As enum_StatusVendedor) As String
  Select Case statusVendedor
      Case eSV_ACTIVO: enumStatusVendedorToStringSiglas = "ACT"
      Case eSV_INACTIVO: enumStatusVendedorToStringSiglas = "IAC"
      Case eSV_RESTRINGIDO: enumStatusVendedorToStringSiglas = "RTR"
      Case Else: enumStatusVendedorToStringSiglas = enumStatusVendedorToStringSiglas(0)
   End Select
End Function

Public Function fSiglasStatusVendedorToEnum(ByVal statusVendedor As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusVendedor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSV_ACTIVO To eSV_RESTRINGIDO
   If UCase(statusVendedor) = UCase(enumStatusVendedorToStringSiglas(mEnum)) Then
      fSiglasStatusVendedorToEnum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   fSiglasStatusVendedorToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusVendedor & " no existe en tipo enumerativo statusVendedor"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoComisionToStringSiglas(TipoComision As enum_TipoComision) As String
   Select Case TipoComision
      Case eTC_PorPorcentaje: enumTipoComisionToStringSiglas = "CPP"
      Case eTC_PorMonto: enumTipoComisionToStringSiglas = "CPM"
      Case Else
         enumTipoComisionToStringSiglas = enumTipoComisionToStringSiglas(0)
   End Select
End Function

Public Function fSiglasTipoComisionToEnum(ByVal TipoComision As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoComision
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTC_PorPorcentaje To eTC_PorMonto
      If UCase(TipoComision) = UCase(enumTipoComisionToStringSiglas(mEnum)) Then
         fSiglasTipoComisionToEnum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   fSiglasTipoComisionToEnum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoComision & " no existe en tipo enumerativo TipoComision"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumTipoComisionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False, Optional ByVal valConSiglas As Boolean = False) As Variant
   Dim varEnum As enum_TipoComision
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoComision.eTC_PorMonto - enum_TipoComision.eTC_PorPorcentaje
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoComision.eTC_PorPorcentaje To enum_TipoComision.eTC_PorMonto
      If valConSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumTipoComisionToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTipoComisionToStringSiglas(varEnum) & varCaracterDelimitador
      End If
        nCount = nCount + 1
   Next
   fEnumTipoComisionToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumRefinanciadoToString(refinanciado As enum_Refinanciado) As String
   Select Case refinanciado
      Case eRe_ES_REFINANCIADO: enumRefinanciadoToString = "Refinanciada"
      Case eRe_SIN_REFINANCIAR: enumRefinanciadoToString = "Sin Refinanciar"
      Case eRe_PROVIENE_REFINANCIAR: enumRefinanciadoToString = "Proviene de Refinanciamiento"
      Case Else: enumRefinanciadoToString = enumRefinanciadoToString(0)
   End Select
End Function

Public Function enumRefinanciadoToSiglas(refinanciado As enum_Refinanciado) As String
   Select Case refinanciado
      Case eRe_ES_REFINANCIADO: enumRefinanciadoToSiglas = "REF"
      Case eRe_SIN_REFINANCIAR: enumRefinanciadoToSiglas = "S/R"
      Case eRe_PROVIENE_REFINANCIAR: enumRefinanciadoToSiglas = "PDR"
      Case Else: enumRefinanciadoToSiglas = enumRefinanciadoToSiglas(0)
   End Select
End Function

Public Function strRefinanciadoToNum(ByVal refinanciado As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Refinanciado
   Dim nCount As Integer
   nCount = 0
   For mEnum = eRe_ES_REFINANCIADO To eRe_PROVIENE_REFINANCIAR
      If UCase(refinanciado) = UCase(enumRefinanciadoToString(mEnum)) Then
         strRefinanciadoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strRefinanciadoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & refinanciado & " no existe en tipo enumerativo refinanciado"
   End If
h_EXIT:
   Exit Function
End Function

Public Function strRefinanciadoToNumToSiglas(ByVal refinanciado As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Refinanciado
   Dim nCount As Integer
   nCount = 0
   For mEnum = eRe_ES_REFINANCIADO To eRe_PROVIENE_REFINANCIAR
      If UCase(refinanciado) = UCase(enumRefinanciadoToSiglas(mEnum)) Then
         strRefinanciadoToNumToSiglas = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strRefinanciadoToNumToSiglas = nCount
h_EXIT:
   Exit Function
End Function

Public Function strRefinanciadoToSiglasToNum(ByVal refinanciado As String) As String
   Dim mEnum As enum_Refinanciado
   Dim nCount As Integer
   nCount = 0
   For mEnum = eRe_ES_REFINANCIADO To eRe_PROVIENE_REFINANCIAR
      If UCase(refinanciado) = UCase(enumRefinanciadoToSiglas(mEnum)) Then
         strRefinanciadoToSiglasToNum = enumRefinanciadoToString(mEnum)
         GoTo h_EXIT
      End If
   Next
   strRefinanciadoToSiglasToNum = ""
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithRefinanciado(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumRefinanciadoToString(enum_Refinanciado.eRe_ES_REFINANCIADO)
   End If
   varComboBox.List(nCount) = enumRefinanciadoToString(enum_Refinanciado.eRe_ES_REFINANCIADO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumRefinanciadoToString(enum_Refinanciado.eRe_SIN_REFINANCIAR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumRefinanciadoToString(enum_Refinanciado.eRe_PROVIENE_REFINANCIAR)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function fEnumRefinanciadoToStringInArray(ByVal conSiglas As Boolean, Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_Refinanciado
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_Refinanciado.eRe_PROVIENE_REFINANCIAR - enum_Refinanciado.eRe_ES_REFINANCIADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_Refinanciado.eRe_ES_REFINANCIADO To enum_Refinanciado.eRe_PROVIENE_REFINANCIAR
      If conSiglas = False Then
         varArray(nCount) = varCaracterDelimitador & enumRefinanciadoToString(varEnum) & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumRefinanciadoToSiglas(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumRefinanciadoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumSalonDeCursoToString(ByVal SalonDeCurso As enum_SalonDeCurso) As String
   Select Case SalonDeCurso
      Case eSDC_SALA1: enumSalonDeCursoToString = "SALA  1"
      Case eSDC_SALA2: enumSalonDeCursoToString = "SALA  2"
      Case eSDC_SALA3: enumSalonDeCursoToString = "SALA  3"
      Case Else
         enumSalonDeCursoToString = enumSalonDeCursoToString(0)
   End Select
End Function

Public Function strSalonDeCursoToNum(ByVal SalonDeCurso As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_SalonDeCurso
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSDC_SALA1 To eSDC_SALA3
      If UCase(SalonDeCurso) = UCase(enumSalonDeCursoToString(mEnum)) Then
         strSalonDeCursoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strSalonDeCursoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & SalonDeCurso & " no existe en tipo enumerativo SalonDeCurso"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithSalonDeCurso(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumSalonDeCursoToString(enum_SalonDeCurso.eSDC_SALA1)
   End If
   varComboBox.List(nCount) = enumSalonDeCursoToString(enum_SalonDeCurso.eSDC_SALA1)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSalonDeCursoToString(enum_SalonDeCurso.eSDC_SALA2)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSalonDeCursoToString(enum_SalonDeCurso.eSDC_SALA3)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
 
Public Function fenumSalonDeCursoToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_SalonDeCurso
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_SalonDeCurso.eSDC_SALA3 - enum_SalonDeCurso.eSDC_SALA1
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_SalonDeCurso.eSDC_SALA1 To enum_SalonDeCurso.eSDC_SALA3
      varArray(nCount) = varCaracterDelimitador & enumSalonDeCursoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumSalonDeCursoToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumModoDeEnvioDeClaveToString(ByRef valModoDeEnvioDeClave As enum_ModoDeEnvioDeClave) As String
   Select Case valModoDeEnvioDeClave
      Case eMDEDC_NO_ASIGNADO: enumModoDeEnvioDeClaveToString = "No Asignado"
      Case eMDEDC_INDETERMINADO: enumModoDeEnvioDeClaveToString = "Indeterminado"
      Case eMDEDC_TELEFONO: enumModoDeEnvioDeClaveToString = "Telefono"
      Case eMDEDC_FAX: enumModoDeEnvioDeClaveToString = "Fax"
      Case eMDEDC_MAIL: enumModoDeEnvioDeClaveToString = "Mail"
      Case eMDEDC_MESSENGER: enumModoDeEnvioDeClaveToString = "Messenger"
      Case eMDEDC_EN_PERSONA: enumModoDeEnvioDeClaveToString = "En Persona"
      Case Else
         enumModoDeEnvioDeClaveToString = enumModoDeEnvioDeClaveToString(0)
   End Select
End Function

Public Function strModoDeEnvioDeClaveToNum(ByVal valModoDeEnvioDeClave As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ModoDeEnvioDeClave
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMDEDC_NO_ASIGNADO To eMDEDC_EN_PERSONA
      If UCase(valModoDeEnvioDeClave) = UCase(enumModoDeEnvioDeClaveToString(mEnum)) Then
         strModoDeEnvioDeClaveToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strModoDeEnvioDeClaveToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valModoDeEnvioDeClave & " no existe en tipo enumerativo ""Modo De Envio De Clave"""
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithModoDeEnvioDeClave(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir, Optional valMostrarIndeterminado As Boolean = False)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_NO_ASIGNADO)
   End If
   varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_NO_ASIGNADO)
   nCount = nCount + 1
   If valMostrarIndeterminado Then
      varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_INDETERMINADO)
      nCount = nCount + 1
   End If
   varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_TELEFONO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_FAX)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_MAIL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_MESSENGER)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumModoDeEnvioDeClaveToString(enum_ModoDeEnvioDeClave.eMDEDC_EN_PERSONA)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
 
Public Function fenumModoDeEnvioDeClaveToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_SalonDeCurso
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_ModoDeEnvioDeClave.eMDEDC_EN_PERSONA - enum_ModoDeEnvioDeClave.eMDEDC_NO_ASIGNADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_ModoDeEnvioDeClave.eMDEDC_NO_ASIGNADO To enum_ModoDeEnvioDeClave.eMDEDC_EN_PERSONA
      varArray(nCount) = varCaracterDelimitador & enumSalonDeCursoToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumModoDeEnvioDeClaveToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fenumTipoLLamadaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoLLamada
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoLLamada.eTL_IMPRESION - enum_TipoLLamada.eTL_VENTA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoLLamada.eTL_VENTA To enum_TipoLLamada.eTL_IMPRESION
      varArray(nCount) = varCaracterDelimitador & enumTipoLLamadaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTipoLLamadaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fenumAccionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_Accion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_Accion.eAc_RELLAMAR - enum_Accion.eAc_NINGUNA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_Accion.eAc_NINGUNA To enum_Accion.eAc_RELLAMAR
      varArray(nCount) = varCaracterDelimitador & enumAccionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumAccionToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumAccionAlAnularFactDeMesesAntToString(AccionAlAnularFactDeMesesAnt As enum_AccionAlAnularFactDeMesesAnt) As String
   Select Case AccionAlAnularFactDeMesesAnt
      Case eAAAFDMA_PermitirAnularSinChequear: enumAccionAlAnularFactDeMesesAntToString = "Permitir  Anular  Sin  Chequear"
      Case eAAAFDMA_PreguntarSiDeseaAnular: enumAccionAlAnularFactDeMesesAntToString = "Preguntar  Si  Desea  Anular"
      Case eAAAFDMA_NoPermitirAnular: enumAccionAlAnularFactDeMesesAntToString = "No  Permitir  Anular"
      Case Else
         enumAccionAlAnularFactDeMesesAntToString = enumAccionAlAnularFactDeMesesAntToString(0)
   End Select
End Function

Public Function strAccionAlAnularFactDeMesesAntToNum(ByVal AccionAlAnularFactDeMesesAnt As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AccionAlAnularFactDeMesesAnt
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAAAFDMA_PermitirAnularSinChequear To eAAAFDMA_NoPermitirAnular
      If UCase(AccionAlAnularFactDeMesesAnt) = UCase(enumAccionAlAnularFactDeMesesAntToString(mEnum)) Then
         strAccionAlAnularFactDeMesesAntToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAccionAlAnularFactDeMesesAntToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & AccionAlAnularFactDeMesesAnt & " no existe en tipo enumerativo AccionAlAnularFactDeMesesAnt"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithAccionAlAnularFactDeMesesAnt(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumAccionAlAnularFactDeMesesAntToString(enum_AccionAlAnularFactDeMesesAnt.eAAAFDMA_PermitirAnularSinChequear)
   End If
   varComboBox.List(nCount) = enumAccionAlAnularFactDeMesesAntToString(enum_AccionAlAnularFactDeMesesAnt.eAAAFDMA_PermitirAnularSinChequear)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumAccionAlAnularFactDeMesesAntToString(enum_AccionAlAnularFactDeMesesAnt.eAAAFDMA_PreguntarSiDeseaAnular)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumAccionAlAnularFactDeMesesAntToString(enum_AccionAlAnularFactDeMesesAnt.eAAAFDMA_NoPermitirAnular)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox) - 1000
End Sub

Public Function fenumTarjetaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_Accion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_Tarjeta.eTa_DEBITO - enum_Tarjeta.eTa_VISA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_Tarjeta.eTa_VISA To enum_Tarjeta.eTa_DEBITO
      varArray(nCount) = varCaracterDelimitador & enumTarjetaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTarjetaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumStatusTalonarioToString(ByVal valStatusTalonario As enum_StatusTalonario) As String
   Select Case valStatusTalonario
      Case eST_ACTIVA: enumStatusTalonarioToString = "Activa"
      Case eST_INACTIVA: enumStatusTalonarioToString = "Inactiva"
      Case Else
         enumStatusTalonarioToString = enumStatusTalonarioToString(0)
         End Select
End Function

Public Function strStatusTAlonarioToNum(ByVal valStatusTalonario As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusTalonario
   Dim nCount As Integer
   nCount = 0
   For mEnum = eST_ACTIVA To eST_INACTIVA
      If UCase(valStatusTalonario) = UCase(enumStatusTalonarioToString(mEnum)) Then
         strStatusTAlonarioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusTAlonarioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valStatusTalonario & " no existe en tipo enumerativo statusTalonario"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusTalonario(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusTalonarioToString(enum_StatusTalonario.eST_ACTIVA)
   End If
   varComboBox.List(nCount) = enumStatusTalonarioToString(enum_StatusTalonario.eST_ACTIVA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusTalonarioToString(enum_StatusTalonario.eST_INACTIVA)
   If action = Buscar Then
      varComboBox.ListIndex = 0
   Else
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTalonarioAUsarToString(ByVal talonario As enum_TalonarioAUsar) As String
   Select Case talonario
      Case eTaU_TALONARIO_UNICO: enumTalonarioAUsarToString = "Talonario Unico"
      Case eTaU_MULTIPLES_TALONARIOS: enumTalonarioAUsarToString = "Multiples Talonarios"
      Case Else: enumTalonarioAUsarToString = enumTalonarioToString(0)
   End Select
End Function

Public Function strTalonarioAUsarToNum(ByVal talonario As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TalonarioAUsar
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTaU_TALONARIO_UNICO To eTaU_MULTIPLES_TALONARIOS
      If UCase(talonario) = UCase(enumTalonarioAUsarToString(mEnum)) Then
         strTalonarioAUsarToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTalonarioAUsarToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & talonario & " no existe en tipo enumerativo Talonario a Usar"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTalonarioAUsar(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTalonarioAUsarToString(enum_TalonarioAUsar.eTaU_TALONARIO_UNICO)
      End If
      varComboBox.List(nCount) = enumTalonarioAUsarToString(enum_TalonarioAUsar.eTaU_TALONARIO_UNICO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTalonarioAUsarToString(enum_TalonarioAUsar.eTaU_MULTIPLES_TALONARIOS)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumStatusVisitaToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusVisita
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusVisita.eSV_REALIZADA - enum_StatusVisita.eSV_VIGENTE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusVisita.eSV_VIGENTE To enum_StatusVisita.eSV_REALIZADA
      varArray(nCount) = varCaracterDelimitador & enumStatusVisitaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusVisitaToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumConsultarDataPorToString(ByVal ConsultarDataPor As enum_ConsultarDataPor) As String
   Select Case ConsultarDataPor
      Case eCDP_DESCUADRE: enumConsultarDataPorToString = "Descuadre"
      Case eCDP_ERROR_DE_SISTEMA: enumConsultarDataPorToString = "Error de Sistema"
      Case eCDP_ERROR_DE_USUARIO: enumConsultarDataPorToString = "Error de Usuario"
      Case eCDP_CONVERSION: enumConsultarDataPorToString = "Conversión"
      Case eCDP_INTEGRACION: enumConsultarDataPorToString = "Integración"
      Case eCDP_CONVERSION_E_INTEGRACION: enumConsultarDataPorToString = "Conversión/Integración"
      Case Else: enumConsultarDataPorToString = enumConsultarDataPorToString(0)
   End Select
End Function

Public Function strConsultarDataPorToNum(ByVal ConsultarDataPor As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_ConsultarDataPor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCDP_DESCUADRE To eCDP_CONVERSION_E_INTEGRACION
      If UCase(ConsultarDataPor) = UCase(enumConsultarDataPorToString(mEnum)) Then
         strConsultarDataPorToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strConsultarDataPorToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ConsultarDataPor & " no existe en tipo enumerativo ConsultarDataPor"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithConsultarDataPor(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_DESCUADRE)
      End If
      varComboBox.List(nCount) = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_DESCUADRE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_ERROR_DE_SISTEMA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_ERROR_DE_USUARIO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_CONVERSION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_INTEGRACION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumConsultarDataPorToString(enum_ConsultarDataPor.eCDP_CONVERSION_E_INTEGRACION)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumConsultarDataPorToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_ConsultarDataPor
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_ConsultarDataPor.eCDP_CONVERSION_E_INTEGRACION - enum_ConsultarDataPor.eCDP_DESCUADRE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_ConsultarDataPor.eCDP_DESCUADRE To enum_ConsultarDataPor.eCDP_CONVERSION_E_INTEGRACION
      varArray(nCount) = varCaracterDelimitador & enumConsultarDataPorToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumConsultarDataPorToStringInArray = varArray
   On Error GoTo 0
End Function


Public Function enumImpresorasFiscalesToString(ByVal ImpresorasFiscales As Enum_ImpresorasFiscales) As String
   Select Case ImpresorasFiscales
      Case eIF_EPSON_PF_220: enumImpresorasFiscalesToString = "EPSON PF-220"
      Case eIF_EPSON_PF_300: enumImpresorasFiscalesToString = "EPSON PF-300"
      Case eIF_EPSON_TM_675_PF: enumImpresorasFiscalesToString = "EPSON TM-675PF"
      Case eIF_EPSON_TM_950_PF: enumImpresorasFiscalesToString = "EPSON TM-950/PF"
      Case eIF_BEMATECH_MP_20_FI_II: enumImpresorasFiscalesToString = "BEMATECH MP-20 FI II"
      Case eIF_SAMSUNG_BIXOLON_SRP_270: enumImpresorasFiscalesToString = "BIXOLON SRP 270"
      Case eIF_EPSON_PF_220II: enumImpresorasFiscalesToString = "EPSON PF-220 II"
      Case eIF_OKI_ML_1120: enumImpresorasFiscalesToString = "OKI ML-1120"
      Case eIF_SAMSUNG_BIXOLON_SRP_350: enumImpresorasFiscalesToString = "BIXOLON SRP 350"
      Case eIF_ACLAS_PP1F3: enumImpresorasFiscalesToString = "ACLAS PP1F3"
      Case eIF_BMC_CAMEL: enumImpresorasFiscalesToString = "BMC CAMEL"
      Case eIF_BMC_SPARK_614: enumImpresorasFiscalesToString = "BMC SPARK 614"
      Case eIF_BEMATECH_MP_2100_FI: enumImpresorasFiscalesToString = "BEMATECH MP-2100 FI"
      Case eIF_DASCOM_TALLY_1125: enumImpresorasFiscalesToString = "DASCOM TALLY 1125"
      Case eIF_QPRINT_MF: enumImpresorasFiscalesToString = "QPRINT MF"
      Case eIF_ELEPOS_VMAX: enumImpresorasFiscalesToString = "ELEPOS VMAX"
      Case eIF_DASCOM_TALLY_DT_230: enumImpresorasFiscalesToString = "DASCOM TALLY DT 230"
      Case eIF_SAMSUNG_BIXOLON_SRP_280: enumImpresorasFiscalesToString = "BIXOLON SRP 280"
      Case eIF_ACLAS_PP9: enumImpresorasFiscalesToString = "ACLAS PP9"
      Case eIF_SAMSUNG_BIXOLON_SRP_812: enumImpresorasFiscalesToString = "BIXOLON SRP 812"
      Case eif_HKA80: enumImpresorasFiscalesToString = "HKA80"
      Case eif_HKA112: enumImpresorasFiscalesToString = "HKA112"
      Case eIF_BMC_TH34_EJ: enumImpresorasFiscalesToString = "BMC TH34 EJ"
      Case Else: enumImpresorasFiscalesToString = enumImpresorasFiscalesToString(0)
   End Select
End Function

Public Function strImpresorasFiscalesToNum(ByVal ImpresorasFiscales As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As Enum_ImpresorasFiscales
   Dim nCount As Integer
   nCount = 0
   For mEnum = Enum_ImpresorasFiscales.eIF_EPSON_PF_220 To Enum_ImpresorasFiscales.eIF_BMC_TH34_EJ
      If UCase(ImpresorasFiscales) = UCase(enumImpresorasFiscalesToString(mEnum)) Then
         strImpresorasFiscalesToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strImpresorasFiscalesToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & ImpresorasFiscales & " no existe en tipo enumerativo ImpresorasFiscales"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithImpresorasFiscales(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_EPSON_PF_220)
      End If
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_EPSON_PF_220)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_EPSON_PF_300)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_EPSON_TM_675_PF)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_EPSON_TM_950_PF)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_BEMATECH_MP_20_FI_II)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_SAMSUNG_BIXOLON_SRP_270)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_EPSON_PF_220II)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_OKI_ML_1120)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_SAMSUNG_BIXOLON_SRP_350)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_ACLAS_PP1F3)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_BMC_CAMEL)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_BMC_SPARK_614)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_BEMATECH_MP_2100_FI)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_DASCOM_TALLY_1125)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_QPRINT_MF)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_ELEPOS_VMAX)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_DASCOM_TALLY_DT_230)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_SAMSUNG_BIXOLON_SRP_280)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_ACLAS_PP9)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_SAMSUNG_BIXOLON_SRP_812)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eif_HKA80)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eif_HKA112)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumImpresorasFiscalesToString(Enum_ImpresorasFiscales.eIF_BMC_TH34_EJ)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumImpresorasFiscalesToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As Enum_ImpresorasFiscales
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = Enum_ImpresorasFiscales.eIF_BMC_TH34_EJ - Enum_ImpresorasFiscales.eIF_EPSON_PF_220
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = Enum_ImpresorasFiscales.eIF_EPSON_PF_220 To Enum_ImpresorasFiscales.eIF_BMC_TH34_EJ
      varArray(nCount) = varCaracterDelimitador & enumImpresorasFiscalesToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumImpresorasFiscalesToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumEstadoImpresionFiscalToString(ByVal EstadoImpresionFiscal As Enum_EstadoImpresionFiscal) As String
   Select Case EstadoImpresionFiscal
      Case eEIF_VERIFICANDO_CONEXION_CON_IMPRESORA: enumEstadoImpresionFiscalToString = "Verificando Conexión con Impresora"
      Case eEIF_CIERRE_X: enumEstadoImpresionFiscalToString = "Cierre X"
      Case eEIF_CIERRE_Z: enumEstadoImpresionFiscalToString = "Cierre Z"
      Case eEIF_OBTENER_SERIAL: enumEstadoImpresionFiscalToString = "Obtener Serial"
      Case eEIF_IMPRIMIR_VENTA: enumEstadoImpresionFiscalToString = "Imprimir Venta"
      Case eEIF_OBTENER_ULTIMO_REPORTE_Z: enumEstadoImpresionFiscalToString = "Obtener Reporte Z"
      Case Else: enumEstadoImpresionFiscalToString = enumEstadoImpresionFiscalToString(0)
   End Select
End Function

Public Function strEstadoImpresionFiscalToNum(ByVal EstadoImpresionFiscal As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As Enum_EstadoImpresionFiscal
   Dim nCount As Integer
   nCount = 0
   For mEnum = Enum_EstadoImpresionFiscal.eEIF_VERIFICANDO_CONEXION_CON_IMPRESORA To Enum_EstadoImpresionFiscal.eEIF_IMPRIMIR_VENTA
      If UCase(EstadoImpresionFiscal) = UCase(enumEstadoImpresionFiscalToString(mEnum)) Then
         strEstadoImpresionFiscalToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strEstadoImpresionFiscalToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & EstadoImpresionFiscal & " no existe en tipo enumerativo EstadoImpresionFiscal"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fenumStatusParticipanteToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusParticipante
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusParticipante.eSP_TRASLADADO - enum_StatusParticipante.eSP_RESERVADO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusParticipante.eSP_RESERVADO To enum_StatusParticipante.eSP_TRASLADADO
      varArray(nCount) = varCaracterDelimitador & enumStatusParticipanteToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumStatusParticipanteToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipoDocumentoIdentificacionPeruToString(ByVal TipoDocumentoIdentificacionPeru As enum_TipoDocumentoIdentificacionPeru) As String
   Select Case TipoDocumentoIdentificacionPeru
      Case eTDIP_RUC: enumTipoDocumentoIdentificacionPeruToString = "RUC"
      Case eTDIP_DNI: enumTipoDocumentoIdentificacionPeruToString = "DNI"
      Case eTDIP_CARNET_DE_EXTRANJERIA: enumTipoDocumentoIdentificacionPeruToString = "Carnet de Extranjería"
      Case eTDIP_PASAPORTE: enumTipoDocumentoIdentificacionPeruToString = "Pasaporte"
      Case eTDIP_OTROS_TIPOS_DE_DOCUMENTOS: enumTipoDocumentoIdentificacionPeruToString = "Otros Documentos"
      Case Else: enumTipoDocumentoIdentificacionPeruToString = enumTipoDocumentoIdentificacionPeruToString(0)
   End Select
End Function

Public Function strTipoDocumentoIdentificacionPeruToNum(ByVal TipoDocumentoIdentificacionPeru As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDocumentoIdentificacionPeru
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDIP_RUC To eTDIP_OTROS_TIPOS_DE_DOCUMENTOS
      If UCase(TipoDocumentoIdentificacionPeru) = UCase(enumTipoDocumentoIdentificacionPeruToString(mEnum)) Then
         strTipoDocumentoIdentificacionPeruToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDocumentoIdentificacionPeruToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDocumentoIdentificacionPeru & " no existe en tipo enumerativo TipoDocumentoIdentificacionPeru"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDocumentoIdentificacionPeru(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC)
      End If
      varComboBox.List(nCount) = enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_DNI)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_CARNET_DE_EXTRANJERIA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_PASAPORTE)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_OTROS_TIPOS_DE_DOCUMENTOS)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fEnumTipoDocumentoIdentificacionPeruToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoIdentificacionPeru
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoIdentificacionPeru.eTDIP_OTROS_TIPOS_DE_DOCUMENTOS - enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC To enum_TipoDocumentoIdentificacionPeru.eTDIP_OTROS_TIPOS_DE_DOCUMENTOS
      varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoIdentificacionPeruToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDocumentoIdentificacionPeruToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fenumTipoDocumentoCxPToStringCompletoInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoCxP
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoCxP.eTCxP_NOTADEENTREGA - enum_TipoDocumentoCxP.eTCxP_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTCxP_FACTURA To eTCxP_NOTADEENTREGA
      varArray(nCount) = varCaracterDelimitador & enumTipodocumentoCxcToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTipoDocumentoCxPToStringCompletoInArray = varArray
   On Error GoTo 0
End Function

Public Function enumTipodocumentoCxcToString(ByVal tipoDocumento As enum_TipoDocumentoCxP) As String
   Select Case tipoDocumento
      Case eTCxP_FACTURA: enumTipodocumentoCxcToString = "Factura"
      Case eTCxP_GIRO: enumTipodocumentoCxcToString = "Giro"
      Case eTCxP_CHEQUEDEVUELTO: enumTipodocumentoCxcToString = "Cheque Devuelto"
      Case eTCxP_NOTADECREDITO: enumTipodocumentoCxcToString = "Nota de Crédito"
      Case eTCxP_NOTADEDEBITO: enumTipodocumentoCxcToString = "Nota de Débito"
      Case eTCxP_NOTADEENTREGA: enumTipodocumentoCxcToString = "Nota de Entrega"
      Case Else: enumTipodocumentoCxcToString = enumTipodocumentoCxcToString(0)
   End Select
End Function

Public Function EnumTipoDocumentoIdentificacionPeruToCodigoPeru(ByVal TipoDocumentoIdentificacionPeru As enum_TipoDocumentoIdentificacionPeru) As String
   Select Case TipoDocumentoIdentificacionPeru
      Case eTDIP_RUC: EnumTipoDocumentoIdentificacionPeruToCodigoPeru = "06"
      Case eTDIP_DNI: EnumTipoDocumentoIdentificacionPeruToCodigoPeru = "01"
      Case eTDIP_CARNET_DE_EXTRANJERIA: EnumTipoDocumentoIdentificacionPeruToCodigoPeru = "04"
      Case eTDIP_PASAPORTE: EnumTipoDocumentoIdentificacionPeruToCodigoPeru = "07"
      Case eTDIP_OTROS_TIPOS_DE_DOCUMENTOS: EnumTipoDocumentoIdentificacionPeruToCodigoPeru = "00"
      Case Else: EnumTipoDocumentoIdentificacionPeruToCodigoPeru = EnumTipoDocumentoIdentificacionPeruToCodigoPeru(0)
   End Select
End Function

Public Function EnumTipoDocumentoToCodigoPeru(ByVal tipoDocumento As enum_TipoDeTransaccion) As String
   Select Case tipoDocumento
      Case eTD_FACTURA: EnumTipoDocumentoToCodigoPeru = "01"
      Case eTD_NOTADECREDITO: EnumTipoDocumentoToCodigoPeru = "07"
      Case eTD_NOTADEDEBITO: EnumTipoDocumentoToCodigoPeru = "08"
      Case eTD_BOLETADEVENTA: EnumTipoDocumentoToCodigoPeru = "03"
      Case eTD_TICKETMAQUINAREGISTRADORA: EnumTipoDocumentoToCodigoPeru = "12"
      Case eTD_RECIBOPORHONORARIOS: EnumTipoDocumentoToCodigoPeru = "02"
      Case eTD_LIQUIDACIONDECOMPRA: EnumTipoDocumentoToCodigoPeru = "04"
      Case eTD_OTROS: EnumTipoDocumentoToCodigoPeru = "99"
      Case Else: EnumTipoDocumentoToCodigoPeru = EnumTipoDocumentoToCodigoPeru(0)
   End Select
End Function

Public Function EnumTipoDeFormatoFechaToString(ByVal TipoDeFormatoFecha As enum_TipoDeFormatoFecha) As String
   Select Case TipoDeFormatoFecha
      Case eCSF_CON_BARRA: EnumTipoDeFormatoFechaToString = "dd/mm/yyyy"
      Case eCSF_CON_GUION: EnumTipoDeFormatoFechaToString = "dd-mm-yyyy"
      Case eCSF_CON_PUNTO: EnumTipoDeFormatoFechaToString = "dd.mm.yyyy"
      Case eCSF_CON_OTRO: EnumTipoDeFormatoFechaToString = "Otro"
      Case Else: EnumTipoDeFormatoFechaToString = EnumTipoDeFormatoFechaToString(0)
   End Select
End Function

Public Function strTipoDeFormatoFechaToNum(ByVal TipoDeFormatoFecha As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeFormatoFecha
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCSF_CON_BARRA To eCSF_CON_OTRO
      If UCase(TipoDeFormatoFecha) = UCase(EnumTipoDeFormatoFechaToString(mEnum)) Then
         strTipoDeFormatoFechaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeFormatoFechaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeFormatoFecha & " no existe en tipo enumerativo TipoDeFormatoFecha"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeFormatoFecha(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = EnumTipoDeFormatoFechaToString(enum_TipoDeFormatoFecha.eCSF_CON_BARRA)
      End If
      varComboBox.List(nCount) = EnumTipoDeFormatoFechaToString(enum_TipoDeFormatoFecha.eCSF_CON_BARRA)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormatoFechaToString(enum_TipoDeFormatoFecha.eCSF_CON_GUION)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormatoFechaToString(enum_TipoDeFormatoFecha.eCSF_CON_PUNTO)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormatoFechaToString(enum_TipoDeFormatoFecha.eCSF_CON_OTRO)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function EnumTipoDeFormaDeCobroToString(ByVal TipoDeFormaDeCobro As enum_TipoDeFormaDeCobro) As String
   Select Case TipoDeFormaDeCobro
      Case eTFC_EFECTIVO: EnumTipoDeFormaDeCobroToString = "Efectivo"
      Case eTFC_TARJETA_DEBITO: EnumTipoDeFormaDeCobroToString = "Tarjeta de Debito"
      Case eTFC_TARJETA_CREDITO: EnumTipoDeFormaDeCobroToString = "Tarjeta de Crédito"
      Case eTFC_CHEQUE: EnumTipoDeFormaDeCobroToString = "Cheque"
      Case eTFC_OTROS: EnumTipoDeFormaDeCobroToString = "Otros"
      Case Else: EnumTipoDeFormaDeCobroToString = EnumTipoDeFormaDeCobroToString(0)
   End Select
End Function

Public Sub FillComboBoxWithTipoDeFormaDeCobro(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_EFECTIVO)
      End If
      varComboBox.List(nCount) = EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_EFECTIVO)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_TARJETA_DEBITO)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_TARJETA_CREDITO)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_CHEQUE)
      nCount = nCount + 1
      varComboBox.List(nCount) = EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_OTROS)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function strTipoDeFormaDeCobroToNum(ByVal TipoDeFormaDeCobro As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeFormaDeCobro
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTFC_EFECTIVO To eTFC_OTROS
      If UCase(TipoDeFormaDeCobro) = UCase(EnumTipoDeFormaDeCobroToString(mEnum)) Then
         strTipoDeFormaDeCobroToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeFormaDeCobroToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeFormaDeCobro & " no existe en tipo enumerativo TipoDeFormaDeCobro"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoDocumentoToString(tipoDocumento As enum_TipoDocumentoIVA) As String
   Select Case tipoDocumento
      Case eTD_TD_FACTURA: enumTipoDocumentoToString = "Factura"
      Case eTD_TD_NOTADEENTREGA: enumTipoDocumentoToString = "Nota de Entrega"
      Case eTD_TD_NOTADECREDITO: enumTipoDocumentoToString = "Nota de Crédito"
      Case eTD_TD_NOTADEDEBITO: enumTipoDocumentoToString = "Nota de Débito"
      Case eTD_TD_RESUMENDIARIODEVENTAS: enumTipoDocumentoToString = "Resumen Diario de Ventas"
      Case eTD_TD_VENTADEPASAJESAEREOS: enumTipoDocumentoToString = "Venta de Pasajes Aéreos"
      Case eTD_TD_RESUMENCONTRIBUYENTES: enumTipoDocumentoToString = "Resumen Contribuyentes"
      Case eTD_TD_FACTURAANULADA: enumTipoDocumentoToString = "Factura Anulada"
      Case eTD_TD_RESUMENNOCONTRIBUYENTES: enumTipoDocumentoToString = "Resumen No Contribuyentes"
      Case eTD_TD_CERTIFICACIONES: enumTipoDocumentoToString = "Certificaciones"
      Case Else
         enumTipoDocumentoToString = enumTipoDocumentoToString(0)
      End Select
End Function

Public Function enumAccionLimiteItemsFacturaToString(AccionLimiteItemsFactura As enum_AccionLimiteItemsFactura) As String
   Select Case AccionLimiteItemsFactura
      Case eALIF_SOLO_ADVERTIR: enumAccionLimiteItemsFacturaToString = "Sólo Advertir"
      Case eALIF_NO_EXEDER_NUMERO_DE_ITEMS: enumAccionLimiteItemsFacturaToString = "No Exceder el Número de Items"
      Case Else
         enumAccionLimiteItemsFacturaToString = enumAccionLimiteItemsFacturaToString(0)
   End Select
End Function

Public Function strAccionLimiteItemsFacturaToNum(ByVal AccionLimiteItemsFactura As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AccionLimiteItemsFactura
   Dim nCount As Integer
   nCount = 0
   For mEnum = eALIF_SOLO_ADVERTIR To eALIF_NO_EXEDER_NUMERO_DE_ITEMS
      If UCase(AccionLimiteItemsFactura) = UCase(enumAccionLimiteItemsFacturaToString(mEnum)) Then
         strAccionLimiteItemsFacturaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAccionLimiteItemsFacturaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & AccionLimiteItemsFactura & " no existe en tipo enumerativo AccionLimiteItemsFactura"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithAccionLimiteItemsFactura(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumAccionLimiteItemsFacturaToString(enum_AccionLimiteItemsFactura.eALIF_SOLO_ADVERTIR)
   End If
   varComboBox.List(nCount) = enumAccionLimiteItemsFacturaToString(enum_AccionLimiteItemsFactura.eALIF_SOLO_ADVERTIR)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumAccionLimiteItemsFacturaToString(enum_AccionLimiteItemsFactura.eALIF_NO_EXEDER_NUMERO_DE_ITEMS)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub


Public Function enumInformeVentasDeTerceroToString(ByVal valEnumInformesVentasATerceros As enum_InformesConVentasDeTercero) As String
   Select Case valEnumInformesVentasATerceros
      Case eIVT_VentasPropias: enumInformeVentasDeTerceroToString = "Ventas Propias"
      Case eIVT_SoloVentasDeTercero: enumInformeVentasDeTerceroToString = "Por Cuenta de Terceros"
      Case eIVT_TodasLasVentas: enumInformeVentasDeTerceroToString = "Todas las ventas"
      Case Else
         enumInformeVentasDeTerceroToString = enumInformeVentasDeTerceroToString(0)
         End Select
End Function
Public Function strInformeVentasDeTerceroToNum(ByVal valInformeVentasDeTercero As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_InformesConVentasDeTercero
   Dim nCount As Integer
   nCount = 0
   For mEnum = eIVT_TodasLasVentas To eIVT_SoloVentasDeTercero
      If UCase(valInformeVentasDeTercero) = UCase(enumInformeVentasDeTerceroToString(mEnum)) Then
         strInformeVentasDeTerceroToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strInformeVentasDeTerceroToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & strInformeVentasDeTerceroToNum & " no existe en tipo enumerativo ProductoLlamada"
   End If
h_EXIT:
   Exit Function
End Function

Public Function strTipoDocumentoIvaToNum(ByVal tipoDocumentoIva As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDocumentoIVA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_TD_FACTURA To eTD_TD_CERTIFICACIONES
      If UCase(tipoDocumentoIva) = UCase(enumTipoDocumentoIvaToString(mEnum)) Then
         strTipoDocumentoIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDocumentoIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDocumentoIva & " no existe en tipo enumerativo tipoDocumentoIva"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumTipoDocumentoDeCompraToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoIVA
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoIVA.eTD_TD_CERTIFICACIONES - enum_TipoDocumentoIVA.eTD_TD_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDocumentoIVA.eTD_TD_FACTURA To enum_TipoDocumentoIVA.eTD_TD_CERTIFICACIONES
      varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoIvaToSiglas(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDocumentoDeCompraToStringInArray = varArray
   On Error GoTo 0
End Function
Public Function strTipoProveedorIvaToNum(ByVal tipoProveedorIva As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoProveedorIvaDeLibrosFiscales
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTP_TP_CONRIF To eTP_TP_NODOMICILIADO
      If UCase(tipoProveedorIva) = UCase(enumTipoProveedorIvaToString(mEnum)) Then
         strTipoProveedorIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoProveedorIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoProveedorIva & " no existe en tipo enumerativo tipoProveedorIva"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoProveedorIvaToString(tipoProveedorIva As enum_TipoProveedorIvaDeLibrosFiscales) As String
   Select Case tipoProveedorIva
      Case eTP_TP_CONRIF: enumTipoProveedorIvaToString = "Con RIF"
      Case eTP_TP_SINRIF: enumTipoProveedorIvaToString = "Sin RIF"
      Case eTP_TP_NORESIDENCIADO: enumTipoProveedorIvaToString = "No Residenciado"
      Case eTP_TP_NODOMICILIADO: enumTipoProveedorIvaToString = "No Domiciliado"
      Case Else
         enumTipoProveedorIvaToString = enumTipoProveedorIvaToString(0)
      End Select
End Function



Public Function enumTipoDocumentoIvaToSiglas(tipoDocumentoIva As enum_TipoDocumentoIVA) As String
   Select Case tipoDocumentoIva
      Case eTD_TD_FACTURA: enumTipoDocumentoIvaToSiglas = "FAC"
      Case eTD_TD_NOTADEENTREGA: enumTipoDocumentoIvaToSiglas = "N/E"
      Case eTD_TD_NOTADECREDITO: enumTipoDocumentoIvaToSiglas = "N/C"
      Case eTD_TD_NOTADEDEBITO: enumTipoDocumentoIvaToSiglas = "N/D"
      Case eTD_TD_RESUMENDIARIODEVENTAS: enumTipoDocumentoIvaToSiglas = "R/D"
      Case eTD_TD_VENTADEPASAJESAEREOS: enumTipoDocumentoIvaToSiglas = "V/P"
      Case eTD_TD_RESUMENCONTRIBUYENTES: enumTipoDocumentoIvaToSiglas = "R/C"
      Case eTD_TD_FACTURAANULADA: enumTipoDocumentoIvaToSiglas = "ANU"
      Case eTD_TD_RESUMENNOCONTRIBUYENTES: enumTipoDocumentoIvaToSiglas = "RNC"
      Case eTD_TD_CERTIFICACIONES: enumTipoDocumentoIvaToSiglas = "CER"
      Case Else: enumTipoDocumentoIvaToSiglas = enumTipoDocumentoIvaToSiglas(0)
   End Select
End Function
Public Function fEnumTipoDeTransaccionDeLibrosFiscalesToCompleteStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeTransaccion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE - enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO To enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE
      varArray(nCount) = varCaracterDelimitador & enumTipoDeTransaccionDeLibrosFiscalesToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeTransaccionDeLibrosFiscalesToCompleteStringInArray = varArray
   On Error GoTo 0
End Function
Public Function fEnumTipoDocumentoDeCompraToDescriptionInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDocumentoIVA
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoDocumentoIVA.eTD_TD_CERTIFICACIONES - enum_TipoDocumentoIVA.eTD_TD_FACTURA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoDocumentoIVA.eTD_TD_FACTURA To enum_TipoDocumentoIVA.eTD_TD_CERTIFICACIONES
      varArray(nCount) = varCaracterDelimitador & enumTipoDocumentoIvaToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDocumentoDeCompraToDescriptionInArray = varArray
   On Error GoTo 0
End Function



Public Function enumStatusRetencionIvaToString(statusRetencionIva As enum_StatusRetencionIva) As String
   Select Case statusRetencionIva
      Case eSR_RETENIDA: enumStatusRetencionIvaToString = "Retenida"
      Case eSR_NORETENIDA: enumStatusRetencionIvaToString = "No Retenida"
      Case Else
         enumStatusRetencionIvaToString = enumStatusRetencionIvaToString(0)
         End Select
End Function
Public Function strStatusRetencionIvaToNum(ByVal statusRetencion As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusRetencionIva
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSR_NORETENIDA To eSR_RETENIDA
      If UCase(statusRetencion) = UCase(enumStatusRetencionIvaToString(mEnum)) Then
         strStatusRetencionIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusRetencionIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusRetencion & " no existe en tipo enumerativo statusRetencion"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumStatusRetencionToSiglas(statusRetencion As enum_StatusRetencionIva) As String
   Select Case statusRetencion
      Case eSR_RETENIDA: enumStatusRetencionToSiglas = "RE"
      Case eSR_NORETENIDA: enumStatusRetencionToSiglas = "PR"
      Case Else
         enumStatusRetencionToSiglas = enumStatusRetencionToSiglas(0)
         End Select
End Function

Public Function siglasStatusRetencionToNum(ByVal statusRetencion As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusRetencionIva
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSR_NORETENIDA To eSR_RETENIDA
      If UCase(statusRetencion) = UCase(enumStatusRetencionToSiglas(mEnum)) Then
         siglasStatusRetencionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   siglasStatusRetencionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusRetencion & " no existe en tipo enumerativo statusRetencion"
   End If
h_EXIT:
   Exit Function
End Function
Public Function enumTipoDeVentaIvaToString(tipoDeVentaIva As enum_TipoDeVentaIva) As String
   Select Case tipoDeVentaIva
      Case eTD_TV_VENTASEXENTAS: enumTipoDeVentaIvaToString = "Exentas"
      Case eTD_TV_VENTASEXPORTACION: enumTipoDeVentaIvaToString = "Exportación"
      Case eTD_TV_VENTASCONTRIBUYENTES: enumTipoDeVentaIvaToString = "A Contribuyentes"
      Case eTD_TV_VENTASNOCONTRIBUYENTES: enumTipoDeVentaIvaToString = "No Contribuyentes"
      Case eTD_TV_VENTASPTOLIBRENE: enumTipoDeVentaIvaToString = "Ventas Pto. Libre"
      Case eTD_TV_VENTASNOCONTRIBPTOLIBRENE: enumTipoDeVentaIvaToString = "No Contrib. Pto Libre"
      Case eTD_TV_VENTASEXONERADAS: enumTipoDeVentaIvaToString = "Exoneradas"
      Case eTD_TV_VENTASNOSUJETAS: enumTipoDeVentaIvaToString = "No Sujetas"
      Case eTD_TV_VENTASSINDERECHOACREDITO: enumTipoDeVentaIvaToString = "Sin Derecho A Crédito"
      Case Else
         enumTipoDeVentaIvaToString = enumTipoDeVentaIvaToString(0)
         End Select
End Function

Public Function strTipoDeVentaIvaToNum(ByVal tipoDeVentaIva As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeVentaIva
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_TV_VENTASEXENTAS To eTD_TV_VENTASSINDERECHOACREDITO
      If UCase(tipoDeVentaIva) = UCase(enumTipoDeVentaToString(mEnum)) Then
         strTipoDeVentaIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeVentaIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeVentaIva & " no existe en tipo enumerativo tipoDeVentaIva"
   End If
h_EXIT:
   Exit Function
End Function

'Public Function enumTipoDeTransaccionDeLibrosFiscalesToString(tipoDeTransaccion As enum_TipoDeTransaccion) As String
'   Select Case tipoDeTransaccion
'      Case eTD_A01_REGISTRO: enumTipoDeTransaccionToString = "01 Registro"
'      Case eTD_A02_COMPLEMENTO: enumTipoDeTransaccionToString = "02 Complemento"
'      Case eTD_A03_ANULACION: enumTipoDeTransaccionToString = "03 Anulación"
'      Case eTD_A04_AJUSTE: enumTipoDeTransaccionToString = "04 Ajuste"
'      Case Else
'         enumTipoDeTransaccionToString = enumTipoDeTransaccionToString(0)
'         End Select
'End Function
'
'Public Function strTipoDeTransaccionToNum(ByVal tipoDeTransaccion As String, _
'      Optional ByVal ShowMessage As Boolean = True) As Integer
'   Dim mEnum As enum_TipoDeTransaccion
'   Dim nCount As Integer
'   nCount = 0
'   For mEnum = eTD_A01_REGISTRO To eTD_A04_AJUSTE
'      If UCase(tipoDeTransaccion) = UCase(enumTipoDeTransaccionToString(mEnum)) Then
'         strTipoDeTransaccionToNum = nCount
'         GoTo h_EXIT
'      End If
'      nCount = nCount + 1
'   Next
'   strTipoDeTransaccionToNum = 0
'   If ShowMessage Then
'      gMessage.ProgrammerMessage "Valor " & tipoDeTransaccion & " no existe en tipo enumerativo tipoDeTransaccion"
'   End If
'h_EXIT:
'   Exit Function
'End Function

Public Function enumStatusVentaIvaToString(statusVentaIva As enum_StatusVentaIva) As String
   Select Case statusVentaIva
      Case eSV_COBRADA: enumStatusVentaIvaToString = "Cobrada"
      Case eSV_PORCOBRAR: enumStatusVentaIvaToString = "Por Cobrar"
      
      Case Else
         enumStatusVentaIvaToString = enumStatusVentaIvaToString(0)
         End Select
End Function

Public Function strStatusVentaIvaToNum(ByVal statusVentaIva As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusVentaIva
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSV_COBRADA To eSV_PORCOBRAR
      If UCase(statusVentaIva) = UCase(enumStatusVentaIvaToString(mEnum)) Then
         strStatusVentaIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusVentaIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusVentaIva & " no existe en tipo enumerativo statusVentaIva"
   End If
h_EXIT:
   Exit Function
End Function
Public Function fEnumTipoDeProveedorDeLibrosFiscalesToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoProveedorIvaDeLibrosFiscales
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipoProveedorIvaDeLibrosFiscales.eTP_TP_NODOMICILIADO - enum_TipoProveedorIvaDeLibrosFiscales.eTP_TP_CONRIF
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipoProveedorIvaDeLibrosFiscales.eTP_TP_CONRIF To enum_TipoProveedorIvaDeLibrosFiscales.eTP_TP_NODOMICILIADO
      If varEnum = eTP_TP_CONRIF Then
         varArray(nCount) = varCaracterDelimitador & "" & varCaracterDelimitador
      Else
         varArray(nCount) = varCaracterDelimitador & enumTipoProveedorIvaToString(varEnum) & varCaracterDelimitador
      End If
      nCount = nCount + 1
   Next
   fEnumTipoDeProveedorDeLibrosFiscalesToStringInArray = varArray
   On Error GoTo 0
End Function
Public Function fEnumCreditoFiscalToStringParaLibroInArray() As Variant
   Dim varEnum As enum_CreditoFiscal
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = "'"
   varLimiteSuperior = enum_CreditoFiscal.eCF_NoDeducible - enum_CreditoFiscal.eCF_DEDUCIBLE
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_CreditoFiscal.eCF_DEDUCIBLE To enum_CreditoFiscal.eCF_NoDeducible
      varArray(nCount) = varCaracterDelimitador & enumCreditoFiscalToSiglasParaLibro(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumCreditoFiscalToStringParaLibroInArray = varArray
   On Error GoTo 0
End Function
Public Function enumCreditoFiscalToSiglasParaLibro(CreditoFiscal As enum_CreditoFiscal) As String
   
   Select Case CreditoFiscal
      Case eCF_DEDUCIBLE: enumCreditoFiscalToSiglasParaLibro = "DE"
      Case eCF_PRORRATEABLE: enumCreditoFiscalToSiglasParaLibro = "PD"
      Case eCF_NoDeducible: enumCreditoFiscalToSiglasParaLibro = "ND"
      Case Else
         enumCreditoFiscalToSiglasParaLibro = enumCreditoFiscalToSiglasParaLibro(0)
      End Select
End Function

Public Function enumStatusCompraIvaToString(statuscompra As enum_StatusCompraIva) As String
   Select Case statuscompra
      Case eSC_PAGADA: enumStatusCompraIvaToString = "Pagada"
      Case eSC_PORPAGAR: enumStatusCompraIvaToString = "Por Pagar"
      Case Else
         enumStatusCompraIvaToString = enumStatusCompraIvaToString(0)
         End Select
End Function

Public Function strStatusCompraIvaToNum(ByVal statuscompraIva As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCompraIva
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSC_PAGADA To eSC_PORPAGAR
      If UCase(statuscompraIva) = UCase(enumStatusCompraIvaToString(mEnum)) Then
         strStatusCompraIvaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusCompraIvaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statuscompraIva & " no existe en tipo enumerativo statusCompraIva"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoDocumentoIvaToString(tipoDocumentoIva As enum_TipoDocumentoIVA) As String
   Select Case tipoDocumentoIva
      Case eTD_TD_FACTURA: enumTipoDocumentoIvaToString = "Factura"
      Case eTD_TD_NOTADEENTREGA: enumTipoDocumentoIvaToString = "Nota de Entrega"
      Case eTD_TD_NOTADECREDITO: enumTipoDocumentoIvaToString = "Nota de Crédito"
      Case eTD_TD_NOTADEDEBITO: enumTipoDocumentoIvaToString = "Nota de Débito"
      Case eTD_TD_RESUMENDIARIODEVENTAS: enumTipoDocumentoIvaToString = "Resumen Diario de Ventas"
      Case eTD_TD_VENTADEPASAJESAEREOS: enumTipoDocumentoIvaToString = "Venta de Pasajes Aéreos"
      Case eTD_TD_RESUMENCONTRIBUYENTES: enumTipoDocumentoIvaToString = "Resumen Contribuyentes"
      Case eTD_TD_FACTURAANULADA: enumTipoDocumentoIvaToString = "Factura Anulada"
      Case eTD_TD_RESUMENNOCONTRIBUYENTES: enumTipoDocumentoIvaToString = "Resumen No Contribuyentes"
      Case eTD_TD_CERTIFICACIONES: enumTipoDocumentoIvaToString = "Certificaciones"
      Case Else
         enumTipoDocumentoIvaToString = enumTipoDocumentoIvaToString(0)
      End Select
End Function


Public Function fEnumTipoDeReporteToString(ByVal valTipoDeReporte As enum_ReporteRPTIVA) As String
   Select Case valTipoDeReporte
      Case eRPT_RelacionDeCompras: fEnumTipoDeReporteToString = "Relación de Compra"
      Case eRPT_RelacionDeVentas: fEnumTipoDeReporteToString = "Relación de Venta"
      Case eRPT_InformeDeCompraVentas: fEnumTipoDeReporteToString = "Informes de Compras y Ventas"
      Case eRPT_InfDeCompraOVentaAUnClientOPro: fEnumTipoDeReporteToString = "Informes de Compras/Ventas a un Proveedor/Cliente"
      Case eRPT_InfCreditosDebitosEnUnMes: fEnumTipoDeReporteToString = "Informes de Créditos y Débitos Detallados"
      Case eRPT_InfCredDebEnUnMesResumido: fEnumTipoDeReporteToString = "Créditos y Débitos Resumido"
      Case eRPT_InfCredDebDescuadrados: fEnumTipoDeReporteToString = "Informe de Créditos y Debitos Descuadrados"
      Case eRPT_PlanillaH99Numero294638: fEnumTipoDeReporteToString = "Forma 02 - Planilla H99 N° 294638"
      Case eRPT_PlanillaH99Numero0070556: fEnumTipoDeReporteToString = "Forma 02 - Planilla H99 N° 0070556"
      Case eRPT_PlanillaH2000Numero0015483: fEnumTipoDeReporteToString = "Forma 02 - Planilla H2000 N° 0015483"
      Case eRPT_Forma30: fEnumTipoDeReporteToString = "Planilla Forma 30"
      Case eRPT_BorradorPlanillaForma30: fEnumTipoDeReporteToString = "Borrador Planilla Forma 30"
      Case eRPT_InformeDeProrrateo: fEnumTipoDeReporteToString = "Informe de Prorrateo"
      Case eRPT_Forma0030: fEnumTipoDeReporteToString = "Planilla Forma 00030 Grafibond"
      Case eRPT_LibroDeCompra: fEnumTipoDeReporteToString = "Libro De Compras Para Contr. Ordinario"
      Case eRPT_LibroDeCompraXLS: fEnumTipoDeReporteToString = "Libro De Compras A XLS"
      Case eRPT_RetencionesDeIvaCompraVenta: fEnumTipoDeReporteToString = "Retenciones de Iva"
      Case eRPT_InformeDeProrrateoNuevoFormato: fEnumTipoDeReporteToString = "Informe de Prorrateo Nuevo Formato"
      Case eRPT_SubRepLibroDeCompraXLS: fEnumTipoDeReporteToString = "Libro de Compras Ajustadas"
      Case eRPT_LibroDeVenta: fEnumTipoDeReporteToString = "Libro de Ventas Para Contr. Ordinarios"
      Case eRPT_LibroDeVentaXLS: fEnumTipoDeReporteToString = "Libro de Ventas A XLS"
      Case eRPT_SubRepLibroDeVentasXLS: fEnumTipoDeReporteToString = "Libro de Ventas Ajustadas"
      Case eRPT_Forma0030olivenca: fEnumTipoDeReporteToString = "Planilla Forma 00030 de Olivenca"
      Case eRPT_PruebaDeImpresion: fEnumTipoDeReporteToString = "Prueba de Impresión - Acerca de"
      Case eRPT_ComprobanteRetencionDeIva: fEnumTipoDeReporteToString = "Comprobante Retención de Iva"
      Case eRPT_ComprobantesAutomaticosModificados: fEnumTipoDeReporteToString = "Listado de Comprobantes Automáticos Modificados"
      Case eRPT_DocumentosSinComprobantes: fEnumTipoDeReporteToString = "Documentos sin Comprobantes"
      Case eRPT_DocumentosContabilizados: fEnumTipoDeReporteToString = "Documentos Contabilizados"
      Case eRPT_ComprobantesCompraVentaDescuadrados: fEnumTipoDeReporteToString = "Informe de Comprobantes de Compra/Venta descuadrados"
      Case eRPT_ComprobantesCompraVentaPorLoteDescuadrados: fEnumTipoDeReporteToString = "Informe de Comprobantes de Compra/Venta Por Lote Descuadrados"
      Case eRPT_LibroDeVentaApaisado: fEnumTipoDeReporteToString = "Libro de Venta (Apaisado)"
      Case eRPT_ResumenDeComprobantes: fEnumTipoDeReporteToString = "Informe de Resumen De Comprobantes"
      Case eRPT_ResumenDeCreditosYDebitosDelPeriodo: fEnumTipoDeReporteToString = "Resumen créditos y Débitos del periodo"
      Case eRPT_ResumenDeCreditosYDebitosDelPeriodoApaisado: fEnumTipoDeReporteToString = "Resumen créditos y Débitos del periodo Apaisado"
      Case eRPT_subRelacionDeVentas: fEnumTipoDeReporteToString = "Ventas Por Cuentade Terceros"
      Case eRPT_LibroDeVentaTerceros: fEnumTipoDeReporteToString = "Libro de Venta a Terceros"
      Case eRPT_LibroDeVentaApaisadoTerceros: fEnumTipoDeReporteToString = "Libro de Venta a Terceros (Apaisado)"
      Case eRPT_informeClientesProveedores: fEnumTipoDeReporteToString = "Informe de Clientes/Proveedores"
      Case eRPT_informeIngresosBrutos: fEnumTipoDeReporteToString = "Informe de Ingresos Brutos"
      Case eRPT_ComprobanteResumenApaisado: fEnumTipoDeReporteToString = "COMPROBANTE DE RETENCION DEL IMPUESTO AL VALOR AGREGADO"
      Case eRPT_InformeRifRepetidoClienteProveedor: fEnumTipoDeReporteToString = "Informe de Rif Repetido en Clientes/Proveedores"
     Case Else: fEnumTipoDeReporteToString = ""
   End Select
End Function



Public Function enumStatusRendicionToString(ByVal StatusRendicion As enum_StatusRendicion) As String
   Select Case StatusRendicion
      Case eSRd_EN_PROCESO: enumStatusRendicionToString = "EN PROCESO"
      Case eSRd_CERRADA: enumStatusRendicionToString = "CERRADA"
      Case eSRd_ANULADA: enumStatusRendicionToString = "ANULADA"
      Case Else: enumStatusRendicionToString = enumStatusRendicionToString(0)
   End Select
End Function

Public Function strStatusRendicionToNum(ByVal StatusRendicion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusRendicion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSRd_EN_PROCESO To eSRd_ANULADA
      If UCase(StatusRendicion) = UCase(enumStatusRendicionToString(mEnum)) Then
         strStatusRendicionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusRendicionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusRendicion & " no existe en tipo enumerativo Status Rendicion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusRendicion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusRendicionToString(enum_StatusRendicion.eSRd_EN_PROCESO)
      End If
      varComboBox.List(nCount) = enumStatusRendicionToString(enum_StatusRendicion.eSRd_EN_PROCESO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusRendicionToString(enum_StatusRendicion.eSRd_CERRADA)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusRendicionToString(enum_StatusRendicion.eSRd_ANULADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fenumStatusRendicionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusRendicion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusRendicion.eSRd_CERRADA - enum_StatusRendicion.eSRd_ANULADA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSRd_EN_PROCESO To eSRd_ANULADA
      varArray(nCount) = varCaracterDelimitador & enumStatusRendicionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumStatusRendicionToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function enumStatusDetraccionToString(ByVal StatusDetraccion As enum_StatusDetraccion) As String
   Select Case StatusDetraccion
      Case eSD_AUTODETRACCION: enumStatusDetraccionToString = "Auto Detracción"
      Case eSD_DETRACCIONPORTERCERO: enumStatusDetraccionToString = "Detracción por Tercero"
      Case eSD_NOAPLICADA: enumStatusDetraccionToString = "No Aplicada"
      Case Else: enumStatusDetraccionToString = enumStatusDetraccionToString(0)
   End Select
End Function

Public Function strStatusDetraccionToNum(ByVal StatusDetraccion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusDetraccion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSD_AUTODETRACCION To eSD_NOAPLICADA
      If UCase(StatusDetraccion) = UCase(enumStatusDetraccionToString(mEnum)) Then
         strStatusDetraccionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusDetraccionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusDetraccion & " no existe en tipo enumerativo Status Detracción"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusDetraccion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   If TypeOf varComboBox Is ComboBox Then
      nCount = 0
      If action = Buscar Then
         varComboBox.Text = gDefGen.getSearchAllComboBox()
         varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
         nCount = nCount + 1
      Else
         varComboBox.Text = enumStatusDetraccionToString(enum_StatusDetraccion.eSD_AUTODETRACCION)
      End If
      varComboBox.List(nCount) = enumStatusDetraccionToString(enum_StatusDetraccion.eSD_AUTODETRACCION)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDetraccionToString(enum_StatusDetraccion.eSD_DETRACCIONPORTERCERO)
      nCount = nCount + 1
      varComboBox.List(nCount) = enumStatusDetraccionToString(enum_StatusDetraccion.eSD_NOAPLICADA)
      varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
      varComboBox.ListIndex = 2 ' Se selecciona el valor por defecto
   End If
End Sub

Public Function fenumStatusDetraccionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusDetraccion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusDetraccion.eSD_AUTODETRACCION - enum_StatusDetraccion.eSD_NOAPLICADA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eSD_AUTODETRACCION To eSD_NOAPLICADA
      varArray(nCount) = varCaracterDelimitador & enumStatusDetraccionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumStatusDetraccionToStringInArray = varArray
   On Error GoTo 0
End Function
Public Function enumTipoDePlanillaImprimirToString(TipoDePlanillaImprimir As enum_TipoDePlanillaImprimir) As String
   Select Case TipoDePlanillaImprimir
      Case eTDPI_FORMA_00030_Nueva_DelCentro_Casillas_Separadas_F2006: enumTipoDePlanillaImprimirToString = "FORMA_00030_Nueva_DelCentro_CasillasSeparadas_F2006"
      Case eTDPI_FORMA_00030_Nueva_Grafibond: enumTipoDePlanillaImprimirToString = "FORMA_00030_Nueva_Grafibond"
      Case eTDPI_FORMA_00030_GRAFICHECK_Casillas_Separadas_F2007: enumTipoDePlanillaImprimirToString = "FORMA_00030_Nueva_GRAFICHECK_Casillas_Separadas_F2007"
      Case eTDPI_FORMA_00030_Nueva_DelCentro_Casillas_Separadas_F2008: enumTipoDePlanillaImprimirToString = "FORMA_00030_Nueva_DelCentro_CasillasSeparadas_F2008"
      Case Else
         enumTipoDePlanillaImprimirToString = enumTipoDePlanillaImprimirToString(0)
   End Select
End Function

Public Function strTipoDePlanillaImprimirToNum(ByVal TipoDePlanillaImprimir As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDePlanillaImprimir
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDPI_FORMA_00030_Nueva_DelCentro_Casillas_Separadas_F2006 To eTDPI_FORMA_00030_Nueva_DelCentro_Casillas_Separadas_F2008
      If UCase(TipoDePlanillaImprimir) = UCase(enumTipoDePlanillaImprimirToString(mEnum)) Then
         strTipoDePlanillaImprimirToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDePlanillaImprimirToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDePlanillaImprimir & " no existe en tipo enumerativo TipoDePlanillaImprimir"
   End If
h_EXIT:
   Exit Function
End Function
Public Function enumHojasDeLaPlanillaToString(HojasDeLaPlanilla As enum_HojasDeLaPlanilla) As String
   Select Case HojasDeLaPlanilla
      Case eHDLP_PrimeraHoja: enumHojasDeLaPlanillaToString = "Primera Hoja"
      Case eHDLP_SegundaHoja: enumHojasDeLaPlanillaToString = "Segunda Hoja"
      Case eHDLP_AmbasHojas: enumHojasDeLaPlanillaToString = "Ambas Hojas"
      Case Else
         enumHojasDeLaPlanillaToString = enumHojasDeLaPlanillaToString(0)
   End Select
End Function
Public Function strHojasDeLaPlanillaToNum(ByVal HojasDeLaPlanilla As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_HojasDeLaPlanilla
   Dim nCount As Integer
   nCount = 0
   For mEnum = eHDLP_PrimeraHoja To eHDLP_AmbasHojas
      If UCase(HojasDeLaPlanilla) = UCase(enumHojasDeLaPlanillaToString(mEnum)) Then
         strHojasDeLaPlanillaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strHojasDeLaPlanillaToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & HojasDeLaPlanilla & " no existe en tipo enumerativo HojasDeLaPlanilla"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumMonedasVenezolanasToString(MonedasVenezolanas As enum_MonedasVenezolanas) As String
   Select Case MonedasVenezolanas
      Case eMV_Bolivar: enumMonedasVenezolanasToString = "Bolívar Viejo"
      Case eMV_BolivarFuerte: enumMonedasVenezolanasToString = "Bolívar"
      Case Else
         enumMonedasVenezolanasToString = enumMonedasVenezolanasToString(0)
   End Select
End Function

Public Function strMonedasVenezolanasToNum(ByVal MonedasVenezolanas As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_MonedasVenezolanas
   Dim nCount As Integer
   nCount = 0
   For mEnum = eMV_Bolivar To eMV_BolivarFuerte
      If UCase(MonedasVenezolanas) = UCase(enumMonedasVenezolanasToString(mEnum)) Then
         strMonedasVenezolanasToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strMonedasVenezolanasToNum = 1
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & MonedasVenezolanas & " no existe en tipo enumerativo MonedasVenezolanas"
   End If
h_EXIT:
   Exit Function
End Function
Public Function fListOfModeloPlanillaForma00030(ByVal valDelimiter) As String
   Dim varEnum As enum_ModeloPlanillaForma00030
   Dim varElement As String
   Dim varList As String
   On Error Resume Next
   For varEnum = eMPF00030_F03_Grafibond To eMPF00030_F05_Grafibond
      varElement = enumModeloPlanillaForma00030ToString(varEnum)
      If (varList = "") Then
         varList = varElement
      Else
         varList = varList & valDelimiter & varElement
      End If
   Next
   fListOfModeloPlanillaForma00030 = varList
   On Error GoTo 0
End Function
Public Sub FillComboBoxWithMonedasVenezolanas(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumMonedasVenezolanasToString(enum_MonedasVenezolanas.eMV_Bolivar)
   End If
   varComboBox.List(nCount) = enumMonedasVenezolanasToString(enum_MonedasVenezolanas.eMV_Bolivar)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumMonedasVenezolanasToString(enum_MonedasVenezolanas.eMV_BolivarFuerte)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoNegocioToString(TipoNegocio As enum_TipoNegocio) As String
   Select Case TipoNegocio
      Case eTN_General: enumTipoNegocioToString = "General"
      Case eTN_TallerMecanico: enumTipoNegocioToString = "Taller Mecanico"
      Case Else
         enumTipoNegocioToString = enumTipoNegocioToString(0)
   End Select
End Function

Public Function strTipoNegocioToNum(ByVal TipoNegocio As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoNegocio
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTN_General To eTN_TallerMecanico
      If UCase(TipoNegocio) = UCase(enumTipoNegocioToString(mEnum)) Then
         strTipoNegocioToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoNegocioToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoNegocio & " no existe en tipo enumerativo TipoNegocio"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoNegocio(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoNegocioToString(enum_TipoNegocio.eTN_General)
   End If
   varComboBox.List(nCount) = enumTipoNegocioToString(enum_TipoNegocio.eTN_General)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoNegocioToString(enum_TipoNegocio.eTN_TallerMecanico)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Sub FillComboBoxWithHojasDeLaPlanilla(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumHojasDeLaPlanillaToString(enum_HojasDeLaPlanilla.eHDLP_PrimeraHoja)
   End If
   varComboBox.List(nCount) = enumHojasDeLaPlanillaToString(enum_HojasDeLaPlanilla.eHDLP_PrimeraHoja)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHojasDeLaPlanillaToString(enum_HojasDeLaPlanilla.eHDLP_SegundaHoja)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumHojasDeLaPlanillaToString(enum_HojasDeLaPlanilla.eHDLP_AmbasHojas)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeOperacionSeniatToString(TipoDeOperacion As enum_TipoDeOperacionSeniat) As String
   Select Case TipoDeOperacion
      Case eTO_NO_APLICA: enumTipoDeOperacionSeniatToString = "No Aplica"
      Case eTO_DIVIDENDO_ACCIONES: enumTipoDeOperacionSeniatToString = "Dividendo en Acciones"
      Case eTO_DIVIDENDO_EFECTIVO: enumTipoDeOperacionSeniatToString = "Dividendo en Efectivo"
      Case eTO_VENTAS_ACCIONES: enumTipoDeOperacionSeniatToString = "Venta de Acciones"
      Case Else
         enumTipoDeOperacionSeniatToString = enumTipoDeOperacionSeniatToString(0)
      End Select
End Function

Public Sub FillComboBoxWithTipoDeOperacionSeniat(ByRef varComboBox As Object)
   Dim nCount As Integer
   nCount = 0
   varComboBox.List(nCount) = enumTipoDeOperacionSeniatToString(enum_TipoDeOperacionSeniat.eTO_NO_APLICA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionSeniatToString(enum_TipoDeOperacionSeniat.eTO_DIVIDENDO_ACCIONES)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionSeniatToString(enum_TipoDeOperacionSeniat.eTO_DIVIDENDO_EFECTIVO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeOperacionSeniatToString(enum_TipoDeOperacionSeniat.eTO_VENTAS_ACCIONES)
   varComboBox.ListIndex = 0
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
'Conversion especial para archivo xml de declaracion del seniat
Public Function enumTipoDeOperacionSeniatToCodigoSeniat(TipoDeOperacion As enum_TipoDeOperacionSeniat) As String
   Select Case TipoDeOperacion
      Case eTO_NO_APLICA: enumTipoDeOperacionSeniatToCodigoSeniat = "00"
      Case eTO_DIVIDENDO_ACCIONES: enumTipoDeOperacionSeniatToCodigoSeniat = "01"
      Case eTO_DIVIDENDO_EFECTIVO: enumTipoDeOperacionSeniatToCodigoSeniat = "02"
      Case eTO_VENTAS_ACCIONES: enumTipoDeOperacionSeniatToCodigoSeniat = "03"
      Case Else
         enumTipoDeOperacionSeniatToCodigoSeniat = enumTipoDeOperacionSeniatToCodigoSeniat(0)
      End Select
End Function

Public Function strTipoDeOperacionSeniatToNum(ByVal TipoDeOperacion As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeContribuyente
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTO_NO_APLICA To eTO_VENTAS_ACCIONES
      If UCase(TipoDeOperacion) = UCase(enumTipoDeOperacionSeniatToString(mEnum)) Then
         strTipoDeOperacionSeniatToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeOperacionSeniatToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeOperacion & " no existe en tipo enumerativo TipoDeOperacionSeniat"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fenumTipoDeOperacionSeniatToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeOperacionSeniat
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   Dim valorStr As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eTO_VENTAS_ACCIONES - eTO_NO_APLICA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTO_NO_APLICA To eTO_VENTAS_ACCIONES
      valorStr = enumTipoDeOperacionSeniatToString(varEnum)
      varArray(nCount) = varCaracterDelimitador & valorStr & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fenumTipoDeOperacionSeniatToStringInArray = varArray
   On Error GoTo 0
End Function

Public Function fEnumTipoDeOperacionSeniatToCodigoSeniatInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipoDeOperacionSeniat
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   Dim valorStr As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = eTO_VENTAS_ACCIONES - eTO_NO_APLICA
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = eTO_NO_APLICA To eTO_VENTAS_ACCIONES
      valorStr = enumTipoDeOperacionSeniatToCodigoSeniat(varEnum)
      varArray(nCount) = varCaracterDelimitador & valorStr & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipoDeOperacionSeniatToCodigoSeniatInArray = varArray
   On Error GoTo 0
End Function

Public Function strTipoDeOperacionSeniatCodigoSeniatToNum(ByVal TipoDeOperacion As String, _
      Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeContribuyente
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTO_NO_APLICA To eTO_VENTAS_ACCIONES
      If UCase(TipoDeOperacion) = UCase(enumTipoDeOperacionSeniatToCodigoSeniat(mEnum)) Then
         strTipoDeOperacionSeniatCodigoSeniatToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeOperacionSeniatCodigoSeniatToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeOperacion & " no existe en tipo enumerativo TipoDeOperacion"
   End If
h_EXIT:
   Exit Function
End Function
Public Function enumStatusNotaEntradaSalidaToString(StatusNotaEntradaSalida As enum_StatusNotaEntradaSalida) As String
   Select Case StatusNotaEntradaSalida
      Case eSNES_Vigente: enumStatusNotaEntradaSalidaToString = "Vigente"
      Case eSNES_Anulada: enumStatusNotaEntradaSalidaToString = "Anulada"
      Case Else
         enumStatusNotaEntradaSalidaToString = enumStatusNotaEntradaSalidaToString(0)
   End Select
End Function

Public Function strStatusNotaEntradaSalidaToNum(ByVal StatusNotaEntradaSalida As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusNotaEntradaSalida
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSNES_Vigente To eSNES_Anulada
      If UCase(StatusNotaEntradaSalida) = UCase(enumStatusNotaEntradaSalidaToString(mEnum)) Then
         strStatusNotaEntradaSalidaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusNotaEntradaSalidaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusNotaEntradaSalida & " no existe en tipo enumerativo StatusNotaEntradaSalida"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusNotaEntradaSalida(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusNotaEntradaSalidaToString(enum_StatusNotaEntradaSalida.eSNES_Vigente)
   End If
   varComboBox.List(nCount) = enumStatusNotaEntradaSalidaToString(enum_StatusNotaEntradaSalida.eSNES_Vigente)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusNotaEntradaSalidaToString(enum_StatusNotaEntradaSalida.eSNES_Anulada)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
 
Public Function enumTipoDeMetodoDeCosteoToString(TipoDeMetodoDeCosteo As enum_TipoDeMetodoDeCosteo) As String
   Select Case TipoDeMetodoDeCosteo
      Case eTDMDC_UltimoCosto: enumTipoDeMetodoDeCosteoToString = "Ultimo Costo"
      Case eTDMDC_CostoPromedio: enumTipoDeMetodoDeCosteoToString = "Costo Promedio"
      Case Else
         enumTipoDeMetodoDeCosteoToString = enumTipoDeMetodoDeCosteoToString(0)
   End Select
End Function

Public Function strTipoDeMetodoDeCosteoToNum(ByVal TipoDeMetodoDeCosteo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeMetodoDeCosteo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDMDC_UltimoCosto To eTDMDC_CostoPromedio
      If UCase(TipoDeMetodoDeCosteo) = UCase(enumTipoDeMetodoDeCosteoToString(mEnum)) Then
         strTipoDeMetodoDeCosteoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeMetodoDeCosteoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeMetodoDeCosteo & " no existe en tipo enumerativo TipoDeMetodoDeCosteo"
   End If
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithTipoDeMetodoDeCosteo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeMetodoDeCosteoToString(enum_TipoDeMetodoDeCosteo.eTDMDC_UltimoCosto)
   End If
   varComboBox.List(nCount) = enumTipoDeMetodoDeCosteoToString(enum_TipoDeMetodoDeCosteo.eTDMDC_UltimoCosto)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeMetodoDeCosteoToString(enum_TipoDeMetodoDeCosteo.eTDMDC_CostoPromedio)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function fEnumTipodeOperacionToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_TipodeOperacion
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_TipodeOperacion.eTO_Retiro - enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO To enum_TipodeOperacion.eTO_Retiro
      varArray(nCount) = varCaracterDelimitador & enumTipoDeOperacionToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumTipodeOperacionToStringInArray = varArray
   On Error GoTo 0
End Function
Public Function enum_AgrupacionParaElLibroDeVentasToString(AgrupacionParaElLibroDeVentas As enum_AgrupacionParaElLibroDeVentas) As String
   Select Case AgrupacionParaElLibroDeVentas
      Case eAPLV_PORRESUMENDEVENTAS: enum_AgrupacionParaElLibroDeVentasToString = "Por Resumen De ventas"
      Case eAPLV_PORTIPODECONTRIBUYENTE: enum_AgrupacionParaElLibroDeVentasToString = "Por Tipo de Contribuyente"
      Case eAPLV_DETALLADOPORFACTURA: enum_AgrupacionParaElLibroDeVentasToString = "Detallado Por Facturas"
      Case Else
         enum_AgrupacionParaElLibroDeVentasToString = enum_AgrupacionParaElLibroDeVentasToString(0)
   End Select
End Function

Public Function strAgrupacionParaElLibroDeVentasToNum(ByVal AgrupacionParaElLibroDeVentas As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AgrupacionParaElLibroDeVentas
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAPLV_PORRESUMENDEVENTAS To eAPLV_DETALLADOPORFACTURA
      If UCase(AgrupacionParaElLibroDeVentas) = UCase(enum_AgrupacionParaElLibroDeVentasToString(mEnum)) Then
         strAgrupacionParaElLibroDeVentasToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAgrupacionParaElLibroDeVentasToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & AgrupacionParaElLibroDeVentas & " no existe en tipo enumerativo AgrupacionParaElLibroDeVentas"
   End If
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithAgrupacionParaElLibroDeVentas(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enum_AgrupacionParaElLibroDeVentasToString(enum_AgrupacionParaElLibroDeVentas.eAPLV_PORRESUMENDEVENTAS)
   End If
   varComboBox.List(nCount) = enum_AgrupacionParaElLibroDeVentasToString(enum_AgrupacionParaElLibroDeVentas.eAPLV_PORTIPODECONTRIBUYENTE)
   nCount = nCount + 1
   varComboBox.List(nCount) = enum_AgrupacionParaElLibroDeVentasToString(enum_AgrupacionParaElLibroDeVentas.eAPLV_DETALLADOPORFACTURA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enum_AgrupacionParaElLibroDeVentasToString(enum_AgrupacionParaElLibroDeVentas.eAPLV_PORRESUMENDEVENTAS)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumCampoAMostrarCodigoRIFToString(CampoAMostrar As enum_CampoAMostrarCodigoRIF) As String
   Select Case CampoAMostrar
      Case eCAMCR_CODIGO: enumCampoAMostrarCodigoRIFToString = "Código"
      Case eCAMCR_RIF: enumCampoAMostrarCodigoRIFToString = "RIF"
      Case Else
         enumCampoAMostrarCodigoRIFToString = enumCampoAMostrarCodigoRIFToString(0)
      End Select
End Function

Public Sub FillComboBoxWithCampoAMostrarCodigoRIF(ByRef varComboBox As Object)
   Dim nCount As Integer
   nCount = 0
   varComboBox.List(nCount) = enumCampoAMostrarCodigoRIFToString(enum_CampoAMostrarCodigoRIF.eCAMCR_CODIGO)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCampoAMostrarCodigoRIFToString(enum_CampoAMostrarCodigoRIF.eCAMCR_RIF)
   varComboBox.ListIndex = 0
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumCantidadDeDecimalesToString(CantidadDeDecimales As enum_CantidadDeDecimales) As String
   Select Case CantidadDeDecimales
      Case eCDD_2: enumCantidadDeDecimalesToString = "2"
      Case eCDD_3: enumCantidadDeDecimalesToString = "3"
      Case eCDD_4: enumCantidadDeDecimalesToString = "4"
      Case Else
         enumCantidadDeDecimalesToString = enumCantidadDeDecimalesToString(0)
   End Select
End Function

Public Function strCantidadDeDecimalesToNum(ByVal CantidadDeDecimales As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_CantidadDeDecimales
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCDD_2 To eCDD_4
      If UCase(CantidadDeDecimales) = UCase(enumCantidadDeDecimalesToString(mEnum)) Then
         strCantidadDeDecimalesToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCantidadDeDecimalesToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & CantidadDeDecimales & " no existe en tipo enumerativo CantidadDeDecimales"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCantidadDeDecimales(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCantidadDeDecimalesToString(enum_CantidadDeDecimales.eCDD_2)
   End If
   varComboBox.List(nCount) = enumCantidadDeDecimalesToString(enum_CantidadDeDecimales.eCDD_2)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCantidadDeDecimalesToString(enum_CantidadDeDecimales.eCDD_3)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCantidadDeDecimalesToString(enum_CantidadDeDecimales.eCDD_4)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumSolicitudGeneradaPorToString(SolicitudGeneradaPor As enum_SolicitudGeneradaPor) As String
   Select Case SolicitudGeneradaPor
      Case eSGP_CalculosDeNomina: enumSolicitudGeneradaPorToString = "Cálculos de Nómina"
      Case eSGP_Vacaciones: enumSolicitudGeneradaPorToString = "Vacaciones"
      Case eSGP_Utilidades: enumSolicitudGeneradaPorToString = "Utilidades"
      Case eSGP_PagoDeIntereses: enumSolicitudGeneradaPorToString = "Pago De Intereses"
      Case eSGP_Fideicomiso: enumSolicitudGeneradaPorToString = "Fideicomiso"
      Case eSGP_Liquidacion: enumSolicitudGeneradaPorToString = "Liquidación"
      Case eSGP_AdelantoDeVacaciones: enumSolicitudGeneradaPorToString = "Adelanto de Vacaciones"
      Case eSGP_AnticipoDePrestaciones: enumSolicitudGeneradaPorToString = "Anticipo de Prestaciones"
      Case eSGP_NoAplica: enumSolicitudGeneradaPorToString = "No Aplica"
      Case Else
         enumSolicitudGeneradaPorToString = enumSolicitudGeneradaPorToString(0)
   End Select
End Function

Public Function strSolicitudGeneradaPorToNum(ByVal SolicitudGeneradaPor As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_SolicitudGeneradaPor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSGP_CalculosDeNomina To eSGP_NoAplica
      If UCase(SolicitudGeneradaPor) = UCase(enumSolicitudGeneradaPorToString(mEnum)) Then
         strSolicitudGeneradaPorToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strSolicitudGeneradaPorToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & SolicitudGeneradaPor & " no existe en tipo enumerado SolicitudGeneradaPor"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithSolicitudGeneradaPor(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_CalculosDeNomina)
   End If
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_CalculosDeNomina)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_Vacaciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_Utilidades)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_PagoDeIntereses)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_Fideicomiso)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_Liquidacion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_AdelantoDeVacaciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_AnticipoDePrestaciones)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumSolicitudGeneradaPorToString(enum_SolicitudGeneradaPor.eSGP_NoAplica)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumStatusSolicitudToString(StatusSolicitud As enum_StatusSolicitud) As String
   Select Case StatusSolicitud
      Case eSS_PorProcesar: enumStatusSolicitudToString = "Por Procesar"
      Case eSS_Procesada: enumStatusSolicitudToString = "Procesada"
      Case eSS_ParcialmenteProcesada: enumStatusSolicitudToString = "Parcialmente  Procesada"
      Case eSS_Anulada: enumStatusSolicitudToString = "Anulada"
      Case Else
         enumStatusSolicitudToString = enumStatusSolicitudToString(0)
   End Select
End Function

Public Function strStatusSolicitudToNum(ByVal StatusSolicitud As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusSolicitud
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSS_PorProcesar To eSS_Anulada
      If UCase(StatusSolicitud) = UCase(enumStatusSolicitudToString(mEnum)) Then
         strStatusSolicitudToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusSolicitudToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusSolicitud & " no existe en tipo enumerativo StatusSolicitud"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusSolicitud(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusSolicitudToString(enum_StatusSolicitud.eSS_PorProcesar)
   End If
   varComboBox.List(nCount) = enumStatusSolicitudToString(enum_StatusSolicitud.eSS_PorProcesar)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusSolicitudToString(enum_StatusSolicitud.eSS_Procesada)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusSolicitudToString(enum_StatusSolicitud.eSS_ParcialmenteProcesada)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusSolicitudToString(enum_StatusSolicitud.eSS_Anulada)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumStatusSolicitudRenglonToString(StatusSolicitudRenglon As enum_StatusSolicitudRenglon) As String
   Select Case StatusSolicitudRenglon
      Case eSSR_PorProcesar: enumStatusSolicitudRenglonToString = "Por Procesar"
      Case eSSR_Procesada: enumStatusSolicitudRenglonToString = "Procesada"
      Case Else
         enumStatusSolicitudRenglonToString = enumStatusSolicitudRenglonToString(0)
   End Select
End Function

Public Function strStatusSolicitudRenglonToNum(ByVal StatusSolicitudRenglon As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusSolicitudRenglon
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSSR_PorProcesar To eSSR_Procesada
      If UCase(StatusSolicitudRenglon) = UCase(enumStatusSolicitudRenglonToString(mEnum)) Then
         strStatusSolicitudRenglonToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusSolicitudRenglonToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusSolicitudRenglon & " no existe en tipo enumerativo StatusSolicitudRenglon"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusSolicitudRenglon(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusSolicitudRenglonToString(enum_StatusSolicitudRenglon.eSSR_PorProcesar)
   End If
   varComboBox.List(nCount) = enumStatusSolicitudRenglonToString(enum_StatusSolicitudRenglon.eSSR_PorProcesar)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusSolicitudRenglonToString(enum_StatusSolicitudRenglon.eSSR_Procesada)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeFormaDePagoSolicitudToString(TipoDeFormaDePagoSolicitud As enum_TipoDeFormaDePagoSolicitud) As String
   Select Case TipoDeFormaDePagoSolicitud
      Case eTDFDPS_Efectivo: enumTipoDeFormaDePagoSolicitudToString = "Efectivo"
      Case eTDFDPS_transferencia: enumTipoDeFormaDePagoSolicitudToString = "transferencia"
      Case eTDFDPS_Cheque: enumTipoDeFormaDePagoSolicitudToString = "Cheque"
      Case Else
         enumTipoDeFormaDePagoSolicitudToString = enumTipoDeFormaDePagoSolicitudToString(0)
   End Select
End Function

Public Function strTipoDeFormaDePagoSolicitudToNum(ByVal TipoDeFormaDePagoSolicitud As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeFormaDePagoSolicitud
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDFDPS_Efectivo To eTDFDPS_Cheque
      If UCase(TipoDeFormaDePagoSolicitud) = UCase(enumTipoDeFormaDePagoSolicitudToString(mEnum)) Then
         strTipoDeFormaDePagoSolicitudToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeFormaDePagoSolicitudToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeFormaDePagoSolicitud & " no existe en tipo enumerativo TipoDeFormaDePagoSolicitud"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeFormaDePagoSolicitud(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeFormaDePagoSolicitudToString(enum_TipoDeFormaDePagoSolicitud.eTDFDPS_Efectivo)
   End If
   varComboBox.List(nCount) = enumTipoDeFormaDePagoSolicitudToString(enum_TipoDeFormaDePagoSolicitud.eTDFDPS_Efectivo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeFormaDePagoSolicitudToString(enum_TipoDeFormaDePagoSolicitud.eTDFDPS_transferencia)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeFormaDePagoSolicitudToString(enum_TipoDeFormaDePagoSolicitud.eTDFDPS_Cheque)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumOrigenCXPoCXCoManualToString(OrigenCXPoCXCoManual As enum_OrigenCXPoCXCoManual) As String
   Select Case OrigenCXPoCXCoManual
      Case eOCXPCXCM_CxP: enumOrigenCXPoCXCoManualToString = "CxP"
      Case eOCXPCXCM_CXC: enumOrigenCXPoCXCoManualToString = "CXC"
      Case eOCXPCXCM_Manual: enumOrigenCXPoCXCoManualToString = "Manual"
      Case Else
         enumOrigenCXPoCXCoManualToString = enumOrigenCXPoCXCoManualToString(0)
   End Select
End Function

Public Function strOrigenCXPoCXCoManualToNum(ByVal OrigenCXPoCXCoManual As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_OrigenCXPoCXCoManual
   Dim nCount As Integer
   nCount = 0
   For mEnum = eOCXPCXCM_CxP To eOCXPCXCM_Manual
      If UCase(OrigenCXPoCXCoManual) = UCase(enumOrigenCXPoCXCoManualToString(mEnum)) Then
         strOrigenCXPoCXCoManualToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strOrigenCXPoCXCoManualToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & OrigenCXPoCXCoManual & " no existe en tipo enumerativo OrigenCXPoCXCoManual"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithOrigenCXPoCXCoManual(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumOrigenCXPoCXCoManualToString(enum_OrigenCXPoCXCoManual.eOCXPCXCM_CxP)
   End If
   varComboBox.List(nCount) = enumOrigenCXPoCXCoManualToString(enum_OrigenCXPoCXCoManual.eOCXPCXCM_CxP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumOrigenCXPoCXCoManualToString(enum_OrigenCXPoCXCoManual.eOCXPCXCM_CXC)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumOrigenCXPoCXCoManualToString(enum_OrigenCXPoCXCoManual.eOCXPCXCM_Manual)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumOrigenToString(Origen As enum_Origen) As String
   Select Case Origen
      Case eO_Pago: enumOrigenToString = "Pago"
      Case eO_cxP: enumOrigenToString = "cx P"
      Case Else
         enumOrigenToString = enumOrigenToString(0)
   End Select
End Function

Public Function strOrigenToNum(ByVal Origen As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_Origen
   Dim nCount As Integer
   nCount = 0
   For mEnum = eO_Pago To eO_cxP
      If UCase(Origen) = UCase(enumOrigenToString(mEnum)) Then
         strOrigenToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strOrigenToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & Origen & " no existe en tipo enumerativo Origen"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithOrigen(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumOrigenToString(enum_Origen.eO_Pago)
   End If
   varComboBox.List(nCount) = enumOrigenToString(enum_Origen.eO_Pago)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumOrigenToString(enum_Origen.eO_cxP)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumCampoCodigoAlternativoDeArticuloToString(CampoCodigoAlternativoDeArticulo As enum_CampoCodigoAlternativoDeArticulo) As String
   Select Case CampoCodigoAlternativoDeArticulo
      Case eCCADA_NoAsignado: enumCampoCodigoAlternativoDeArticuloToString = "No Asignado"
      Case eCCADA_CampoDefinible1: enumCampoCodigoAlternativoDeArticuloToString = "Campo Definible 1"
      Case eCCADA_CampoDefinible2: enumCampoCodigoAlternativoDeArticuloToString = "Campo Definible 2"
      Case eCCADA_CampoDefinible3: enumCampoCodigoAlternativoDeArticuloToString = "Campo Definible 3"
      Case eCCADA_CampoDefinible4: enumCampoCodigoAlternativoDeArticuloToString = "Campo Definible 4"
      Case Else
         enumCampoCodigoAlternativoDeArticuloToString = enumCampoCodigoAlternativoDeArticuloToString(0)
   End Select
End Function

Public Function strCampoCodigoAlternativoDeArticuloToNum(ByVal CampoCodigoAlternativoDeArticulo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_CampoCodigoAlternativoDeArticulo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eCCADA_NoAsignado To eCCADA_CampoDefinible4
      If UCase(CampoCodigoAlternativoDeArticulo) = UCase(enumCampoCodigoAlternativoDeArticuloToString(mEnum)) Then
         strCampoCodigoAlternativoDeArticuloToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strCampoCodigoAlternativoDeArticuloToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & CampoCodigoAlternativoDeArticulo & " no existe en tipo enumerado CampoCodigoAlternativoDeArticulo"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithCampoCodigoAlternativoDeArticulo(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumCampoCodigoAlternativoDeArticuloToString(enum_CampoCodigoAlternativoDeArticulo.eCCADA_NoAsignado)
   End If
   varComboBox.List(nCount) = enumCampoCodigoAlternativoDeArticuloToString(enum_CampoCodigoAlternativoDeArticulo.eCCADA_NoAsignado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCampoCodigoAlternativoDeArticuloToString(enum_CampoCodigoAlternativoDeArticulo.eCCADA_CampoDefinible1)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCampoCodigoAlternativoDeArticuloToString(enum_CampoCodigoAlternativoDeArticulo.eCCADA_CampoDefinible2)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCampoCodigoAlternativoDeArticuloToString(enum_CampoCodigoAlternativoDeArticulo.eCCADA_CampoDefinible3)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumCampoCodigoAlternativoDeArticuloToString(enum_CampoCodigoAlternativoDeArticulo.eCCADA_CampoDefinible4)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeTransaccionMunicipalToString(TipoDeTransaccionMunicipal As enum_TipoDeTransaccionMunicipal) As String
   Select Case TipoDeTransaccionMunicipal
      Case eTDTM_NoAplica: enumTipoDeTransaccionMunicipalToString = "No Aplica"
      Case eTDTM_Compra: enumTipoDeTransaccionMunicipalToString = "Compra"
      Case eTDTM_Servicio: enumTipoDeTransaccionMunicipalToString = "Servicio"
      Case Else
         enumTipoDeTransaccionMunicipalToString = enumTipoDeTransaccionMunicipalToString(0)
   End Select
End Function

Public Function strTipoDeTransaccionMunicipalToNum(ByVal TipoDeTransaccionMunicipal As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeTransaccionMunicipal
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDTM_NoAplica To eTDTM_Servicio
      If UCase(TipoDeTransaccionMunicipal) = UCase(enumTipoDeTransaccionMunicipalToString(mEnum)) Then
         strTipoDeTransaccionMunicipalToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeTransaccionMunicipalToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeTransaccionMunicipal & " no existe en tipo enumerado TipoDeTransaccionMunicipal"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeTransaccionMunicipal(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeTransaccionMunicipalToString(enum_TipoDeTransaccionMunicipal.eTDTM_NoAplica)
   End If
   varComboBox.List(nCount) = enumTipoDeTransaccionMunicipalToString(enum_TipoDeTransaccionMunicipal.eTDTM_NoAplica)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeTransaccionMunicipalToString(enum_TipoDeTransaccionMunicipal.eTDTM_Compra)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeTransaccionMunicipalToString(enum_TipoDeTransaccionMunicipal.eTDTM_Servicio)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumFacturaGeneraNotaEntregaToString(FacturaGeneraNotaEntrega As enum_FacturaGeneraNotaEntrega) As String
   Select Case FacturaGeneraNotaEntrega
      Case eFGNE_NoGenerada: enumFacturaGeneraNotaEntregaToString = "0"
      Case eFGNE_Generada: enumFacturaGeneraNotaEntregaToString = "1"
      Case Else
         enumFacturaGeneraNotaEntregaToString = enumFacturaGeneraNotaEntregaToString(0)
   End Select
End Function

Public Function strFacturaGeneraNotaEntregaToNum(ByVal FacturaGeneraNotaEntrega As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FacturaGeneraNotaEntrega
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFGNE_NoGenerada To eFGNE_Generada
      If UCase(FacturaGeneraNotaEntrega) = UCase(enumFacturaGeneraNotaEntregaToString(mEnum)) Then
         strFacturaGeneraNotaEntregaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFacturaGeneraNotaEntregaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & FacturaGeneraNotaEntrega & " no existe en tipo enumerado FacturaGeneraNotaEntrega"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoDeDocumentoRendicionToString(TipoDeDocumentoRendicion As enum_TipoDeDocumentoRendicion) As String
   Select Case TipoDeDocumentoRendicion
      Case eTDDR_Rendicion: enumTipoDeDocumentoRendicionToString = "Rendición"
      Case eTDDR_Reposicion: enumTipoDeDocumentoRendicionToString = "Reposición"
      Case Else
         enumTipoDeDocumentoRendicionToString = enumTipoDeDocumentoRendicionToString(0)
   End Select
End Function
Public Function strTipoDeDocumentoRendicionToNum(ByVal TipoDeDocumentoRendicion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeDocumentoRendicion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDDR_Rendicion To eTDDR_Reposicion
      If UCase(TipoDeDocumentoRendicion) = UCase(enumTipoDeDocumentoRendicionToString(mEnum)) Then
         strTipoDeDocumentoRendicionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeDocumentoRendicionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeDocumentoRendicion & " no existe en tipo enumerativo TipoDeDocumentoRendicion"
   End If
h_EXIT:
   Exit Function
End Function
Public Sub FillComboBoxWithTipoDeDocumentoRendicion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeDocumentoRendicionToString(enum_TipoDeDocumentoRendicion.eTDDR_Rendicion)
   End If
   varComboBox.List(nCount) = enumTipoDeDocumentoRendicionToString(enum_TipoDeDocumentoRendicion.eTDDR_Rendicion)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeDocumentoRendicionToString(enum_TipoDeDocumentoRendicion.eTDDR_Reposicion)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoConexionToString(valTipoConexion As enum_TipoConexion) As String
   Select Case valTipoConexion
      Case enum_TipoConexion.eTDC_PUERTO_SERIAL: enumTipoConexionToString = "Puerto Serial"
      Case enum_TipoConexion.eTDC_USB: enumTipoConexionToString = "USB"
      Case enum_TipoConexion.eTDC_LAN: enumTipoConexionToString = "LAN"
      Case Else: enumTipoConexionToString = enumTipoConexionToString(0)
   End Select
End Function

Public Function strTipoConexionToNum(ByVal valTipoConexion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoConexion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDC_PUERTO_SERIAL To eTDC_LAN
      If UCase(valTipoConexion) = UCase(enumTipoConexionToString(mEnum)) Then
         strTipoConexionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoConexionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valTipoConexion & " no existe en tipo enumerativo tipoConexion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoConexion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoConexionToString(enum_TipoConexion.eTDC_PUERTO_SERIAL)
   End If
   varComboBox.List(nCount) = enumTipoConexionToString(enum_TipoConexion.eTDC_PUERTO_SERIAL)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoConexionToString(enum_TipoConexion.eTDC_USB)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoConexionToString(enum_TipoConexion.eTDC_LAN)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeCompraEspecialToString(TipoDeCompraEspecial As enum_TipoDeCompraEspecial) As String
   Select Case TipoDeCompraEspecial
      Case eTDCE_NoEs: enumTipoDeCompraEspecialToString = "No Es"
      Case eTDCE_Informativa: enumTipoDeCompraEspecialToString = "Informativa"
      Case eTDCE_SueldosySalarios: enumTipoDeCompraEspecialToString = "Sueldos y  Salarios"
      Case Else
         enumTipoDeCompraEspecialToString = enumTipoDeCompraEspecialToString(0)
   End Select
End Function

Public Function strTipoDeCompraEspecialToNum(ByVal TipoDeCompraEspecial As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCompraEspecial
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDCE_NoEs To eTDCE_SueldosySalarios
      If UCase(TipoDeCompraEspecial) = UCase(enumTipoDeCompraEspecialToString(mEnum)) Then
         strTipoDeCompraEspecialToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCompraEspecialToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeCompraEspecial & " no existe en tipo enumerado TipoDeCompraEspecial"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeCompraEspecial(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_NoEs)
   End If
   varComboBox.List(nCount) = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_NoEs)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_Informativa)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCompraEspecialToString(enum_TipoDeCompraEspecial.eTDCE_SueldosySalarios)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumStatusControlDespachoToString(StatusControl As enum_StatusControlDespacho) As String
   Select Case StatusControl
      Case eSCD_PorProcesar: enumStatusControlDespachoToString = "Por Procesar"
      Case eSCD_Procesada: enumStatusControlDespachoToString = "Procesada"
      Case eSCD_Anulada: enumStatusControlDespachoToString = "Anulada"
      Case Else
         enumStatusControlDespachoToString = enumStatusControlDespachoToString(0)
   End Select
End Function

Public Function strStatusControlDespachoToNum(ByVal StatusControlDespacho As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusControlDespacho
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSCD_PorProcesar To eSCD_Anulada
      If UCase(StatusControlDespacho) = UCase(enumStatusControlDespachoToString(mEnum)) Then
         strStatusControlDespachoToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusControlDespachoToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusControlDespacho & " no existe en tipo enumerado StatusControlDespacho"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusControlDespacho(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_PorProcesar)
   End If
   varComboBox.List(nCount) = enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_PorProcesar)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_Procesada)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusControlDespachoToString(enum_StatusControlDespacho.eSCD_Anulada)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumBloquearEmisionToString(ByVal valBloquearEmision As enum_BloquearEmision) As String
   Select Case valBloquearEmision
      Case eBE_NoBloquear: enumBloquearEmisionToString = "No Bloquear"
      Case eBE_BloquearPorCaja: enumBloquearEmisionToString = "Bloquear Por Caja"
      Case eBE_BloqueoGeneral: enumBloquearEmisionToString = "Bloqueo General"
      Case Else
         enumBloquearEmisionToString = enumBloquearEmisionToString(0)
   End Select
End Function

Public Function strBloquearEmisionToNum(ByVal valBloquearEmisionStr As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_BloquearEmision
   Dim nCount As Integer
   nCount = 0
   For mEnum = eBE_BloqueoGeneral To eBE_NoBloquear
      If UCase(valBloquearEmisionStr) = UCase(enumBloquearEmisionToString(mEnum)) Then
         strBloquearEmisionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strBloquearEmisionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valBloquearEmisionStr & " no existe en tipo enumerado BloquearEmisionStr"
   End If
h_EXIT:
   Exit Function
End Function
Public Function enumTipoDeDocumentoCobranzaToString(TipoDeDocumentoCobranza As enum_TipoDeDocumentoCobranza) As String
   Select Case TipoDeDocumentoCobranza
      Case eTDDC_CobranzaDeFactura: enumTipoDeDocumentoCobranzaToString = "Cobranza De Factura"
      Case eTDDC_CobranzaPorAplicacionDeRetencion: enumTipoDeDocumentoCobranzaToString = "Cobranza Por Aplicacion De Retencion"
      Case Else
         enumTipoDeDocumentoCobranzaToString = enumTipoDeDocumentoCobranzaToString(0)
   End Select
End Function

Public Function strTipoDeDocumentoCobranzaToNum(ByVal TipoDeDocumentoCobranza As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeDocumentoCobranza
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDDC_CobranzaDeFactura To eTDDC_CobranzaPorAplicacionDeRetencion
      If UCase(TipoDeDocumentoCobranza) = UCase(enumTipoDeDocumentoCobranzaToString(mEnum)) Then
         strTipoDeDocumentoCobranzaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeDocumentoCobranzaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeDocumentoCobranza & " no existe en tipo enumerado TipoDeDocumentoCobranza"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeDocumentoCobranza(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeDocumentoCobranzaToString(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaDeFactura)
   End If
   varComboBox.List(nCount) = enumTipoDeDocumentoCobranzaToString(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaDeFactura)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeDocumentoCobranzaToString(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumAplicacionAlicuotaToString(ByVal valAplicacionAlicuota As enum_AplicacionAlicuota) As String
   Select Case valAplicacionAlicuota
      Case eAA_NoAplica: enumAplicacionAlicuotaToString = "No Aplica"
      Case eAA_SustituirAlicuotaReducida: enumAplicacionAlicuotaToString = "Sustituir Alicuota Reducida"
      Case eAA_SustituirAlicuotaAdicional: enumAplicacionAlicuotaToString = "Sustituir Alicuota Adicional"
      Case Else
         enumAplicacionAlicuotaToString = enumAplicacionAlicuotaToString(0)
   End Select
End Function

Public Function strAplicacionAlicuotaToNum(ByVal valAplicacionAlicuotaStr As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AplicacionAlicuota
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAA_NoAplica To eAA_SustituirAlicuotaAdicional
      If UCase(valAplicacionAlicuotaStr) = UCase(enumAplicacionAlicuotaToString(mEnum)) Then
         strAplicacionAlicuotaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAplicacionAlicuotaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & valAplicacionAlicuotaStr & " no existe en tipo enumerado AplicacionAlicuotaStr"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumFechaSugeridaRetencionCxPToString(FechaSugeridaRetencionCxP As enum_FechaSugeridaRetencionCxP) As String
   Select Case FechaSugeridaRetencionCxP
      Case eFSRCP_FechaFacturaCxP: enumFechaSugeridaRetencionCxPToString = "Fecha de la Factura de la CxP"
      Case eFSRCP_FechaRegistroCxP: enumFechaSugeridaRetencionCxPToString = "Fecha de Registro de la CxP"
      Case Else
         enumFechaSugeridaRetencionCxPToString = enumFechaSugeridaRetencionCxPToString(0)
   End Select
End Function

Public Function strFechaSugeridaRetencionCxPToNum(ByVal FechaSugeridaRetencionCxP As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_FechaSugeridaRetencionCxP
   Dim nCount As Integer
   nCount = 0
   For mEnum = eFSRCP_FechaFacturaCxP To eFSRCP_FechaRegistroCxP
      If UCase(FechaSugeridaRetencionCxP) = UCase(enumFechaSugeridaRetencionCxPToString(mEnum)) Then
         strFechaSugeridaRetencionCxPToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strFechaSugeridaRetencionCxPToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & FechaSugeridaRetencionCxP & " no existe en tipo enumerado FechaSugeridaRetencionCxP"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithFechaSugeridaRetencionCxP(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumFechaSugeridaRetencionCxPToString(enum_FechaSugeridaRetencionCxP.eFSRCP_FechaFacturaCxP)
   End If
   varComboBox.List(nCount) = enumFechaSugeridaRetencionCxPToString(enum_FechaSugeridaRetencionCxP.eFSRCP_FechaFacturaCxP)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumFechaSugeridaRetencionCxPToString(enum_FechaSugeridaRetencionCxP.eFSRCP_FechaRegistroCxP)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumEstatusAlicuotaImpuestoEspecialToString(EstatusAlicuotaImpuestoEspecial As enum_EstatusAlicuotaImpuestoEspecial) As String
   Select Case EstatusAlicuotaImpuestoEspecial
      Case eEAIE_Activo: enumEstatusAlicuotaImpuestoEspecialToString = "Activo"
      Case eEAIE_Inactivo: enumEstatusAlicuotaImpuestoEspecialToString = "Inactivo"
      Case Else
         enumEstatusAlicuotaImpuestoEspecialToString = enumEstatusAlicuotaImpuestoEspecialToString(0)
   End Select
End Function

Public Function strEstatusAlicuotaImpuestoEspecialToNum(ByVal EstatusAlicuotaImpuestoEspecial As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_EstatusAlicuotaImpuestoEspecial
   Dim nCount As Integer
   nCount = 0
   For mEnum = eEAIE_Activo To eEAIE_Inactivo
      If UCase(EstatusAlicuotaImpuestoEspecial) = UCase(enumEstatusAlicuotaImpuestoEspecialToString(mEnum)) Then
         strEstatusAlicuotaImpuestoEspecialToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strEstatusAlicuotaImpuestoEspecialToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & EstatusAlicuotaImpuestoEspecial & " no existe en tipo enumerado EstatusAlicuotaImpuestoEspecial"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithEstatusAlicuotaImpuestoEspecial(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumEstatusAlicuotaImpuestoEspecialToString(enum_EstatusAlicuotaImpuestoEspecial.eEAIE_Activo)
   End If
   varComboBox.List(nCount) = enumEstatusAlicuotaImpuestoEspecialToString(enum_EstatusAlicuotaImpuestoEspecial.eEAIE_Activo)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumEstatusAlicuotaImpuestoEspecialToString(enum_EstatusAlicuotaImpuestoEspecial.eEAIE_Inactivo)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumAplicaAContribuyenteToString(AplicaAContribuyente As enum_AplicaAContribuyente) As String
   Select Case AplicaAContribuyente
      Case eAAC_TodosLosContribuyentes: enumAplicaAContribuyenteToString = "Todos  Los  Contribuyentes"
      Case eAAC_SoloPersonasNaturales: enumAplicaAContribuyenteToString = "Solo  Personas  Naturales"
      Case eAAC_SoloPersonasJuridicas: enumAplicaAContribuyenteToString = "Solo  Personas  Juridicas"
      Case Else
         enumAplicaAContribuyenteToString = enumAplicaAContribuyenteToString(0)
   End Select
End Function

Public Function strAplicaAContribuyenteToNum(ByVal AplicaAContribuyente As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AplicaAContribuyente
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAAC_TodosLosContribuyentes To eAAC_SoloPersonasJuridicas
      If UCase(AplicaAContribuyente) = UCase(enumAplicaAContribuyenteToString(mEnum)) Then
         strAplicaAContribuyenteToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAplicaAContribuyenteToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & AplicaAContribuyente & " no existe en tipo enumerado AplicaAContribuyente"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithAplicaAContribuyente(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumAplicaAContribuyenteToString(enum_AplicaAContribuyente.eAAC_TodosLosContribuyentes)
   End If
   varComboBox.List(nCount) = enumAplicaAContribuyenteToString(enum_AplicaAContribuyente.eAAC_TodosLosContribuyentes)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumAplicaAContribuyenteToString(enum_AplicaAContribuyente.eAAC_SoloPersonasNaturales)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumAplicaAContribuyenteToString(enum_AplicaAContribuyente.eAAC_SoloPersonasJuridicas)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumAlicuotaIVAToString(AlicuotaIVA As enum_AlicuotaIVA) As String
   Select Case AlicuotaIVA
      Case eAIVA_Reducida: enumAlicuotaIVAToString = "Reducida"
      Case eAIVA_Extendida: enumAlicuotaIVAToString = "Extendida"
      Case Else
         enumAlicuotaIVAToString = enumAlicuotaIVAToString(0)
   End Select
End Function

Public Function strAlicuotaIVAToNum(ByVal AlicuotaIVA As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_AlicuotaIVA
   Dim nCount As Integer
   nCount = 0
   For mEnum = eAIVA_Reducida To eAIVA_Extendida
      If UCase(AlicuotaIVA) = UCase(enumAlicuotaIVAToString(mEnum)) Then
         strAlicuotaIVAToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strAlicuotaIVAToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & AlicuotaIVA & " no existe en tipo enumerado AlicuotaIVA"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithAlicuotaIVA(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumAlicuotaIVAToString(enum_AlicuotaIVA.eAIVA_Reducida)
   End If
   varComboBox.List(nCount) = enumAlicuotaIVAToString(enum_AlicuotaIVA.eAIVA_Reducida)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumAlicuotaIVAToString(enum_AlicuotaIVA.eAIVA_Extendida)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumBaseCalculoParaAlicuotaEspecialToString(BaseCalculoParaAlicuotaEspecial As enum_BaseCalculoParaAlicuotaEspecial) As String
   Select Case BaseCalculoParaAlicuotaEspecial
      Case eBCPAE_TotalFactura: enumBaseCalculoParaAlicuotaEspecialToString = "Total  Factura"
      Case eBCPAE_MontoExentoMasBaseImponible: enumBaseCalculoParaAlicuotaEspecialToString = "Monto  Exento Más  Base  Imponible"
      Case eBCPAE_SoloBaseImponible: enumBaseCalculoParaAlicuotaEspecialToString = "Solo  Base  Imponible"
      Case eBCPAE_SoloBaseImponibleAlicuotaGeneral: enumBaseCalculoParaAlicuotaEspecialToString = "Solo  Base  Imponible  Alícuota  General"
      Case Else
         enumBaseCalculoParaAlicuotaEspecialToString = enumBaseCalculoParaAlicuotaEspecialToString(0)
   End Select
End Function

Public Function strBaseCalculoParaAlicuotaEspecialToNum(ByVal BaseCalculoParaAlicuotaEspecial As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_BaseCalculoParaAlicuotaEspecial
   Dim nCount As Integer
   nCount = 0
   For mEnum = eBCPAE_TotalFactura To eBCPAE_SoloBaseImponibleAlicuotaGeneral
      If UCase(BaseCalculoParaAlicuotaEspecial) = UCase(enumBaseCalculoParaAlicuotaEspecialToString(mEnum)) Then
         strBaseCalculoParaAlicuotaEspecialToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strBaseCalculoParaAlicuotaEspecialToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & BaseCalculoParaAlicuotaEspecial & " no existe en tipo enumerado BaseCalculoParaAlicuotaEspecial"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithBaseCalculoParaAlicuotaEspecial(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumBaseCalculoParaAlicuotaEspecialToString(enum_BaseCalculoParaAlicuotaEspecial.eBCPAE_TotalFactura)
   End If
   varComboBox.List(nCount) = enumBaseCalculoParaAlicuotaEspecialToString(enum_BaseCalculoParaAlicuotaEspecial.eBCPAE_TotalFactura)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumBaseCalculoParaAlicuotaEspecialToString(enum_BaseCalculoParaAlicuotaEspecial.eBCPAE_MontoExentoMasBaseImponible)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumBaseCalculoParaAlicuotaEspecialToString(enum_BaseCalculoParaAlicuotaEspecial.eBCPAE_SoloBaseImponible)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumBaseCalculoParaAlicuotaEspecialToString(enum_BaseCalculoParaAlicuotaEspecial.eBCPAE_SoloBaseImponibleAlicuotaGeneral)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub
Public Function enumTipoDeAlicuotaEspecialToString(tipoDeAlicuota As enum_TipoDeAlicuotaEspecial) As String
   Select Case tipoDeAlicuota = eTDEsp_2
      Case eTDEsp_No: enumTipoDeAlicuotaEspecialToString = "Ninguna"
      Case eTDEsp_1: enumTipoDeAlicuotaEspecialToString = "Alícuota Reducida 1"
      Case eTDEsp_2: enumTipoDeAlicuotaEspecialToString = "Alícuota Reducida 2"
      Case Else
         enumTipoDeAlicuotaEspecialToString = enumTipoDeAlicuotaEspecialToString(0)
   End Select
End Function

Public Function enumStatusCompaniaToString(StatusCompania As enum_StatusCompania) As String
   Select Case StatusCompania
      Case eSCom_Activa: enumStatusCompaniaToString = "Activa"
      Case eSCom_Inactiva: enumStatusCompaniaToString = "Inactiva"
      Case Else
         enumStatusCompaniaToString = enumStatusCompaniaToString(0)
   End Select
End Function

Public Function strStatusCompaniaToNum(ByVal StatusCompania As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusCompania
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSCom_Activa To eSCom_Inactiva
      If UCase(StatusCompania) = UCase(enumStatusCompaniaToString(mEnum)) Then
         strStatusCompaniaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strStatusCompaniaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & StatusCompania & " no existe en tipo enumerado StatusCompania"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithStatusCompania(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumStatusCompaniaToString(enum_StatusCompania.eSCom_Activa)
   End If
   varComboBox.List(nCount) = enumStatusCompaniaToString(enum_StatusCompania.eSCom_Activa)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumStatusCompaniaToString(enum_StatusCompania.eSCom_Inactiva)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeMercanciaToString(TipoDeMercancia As enum_TipoDeMercancia) As String
   Select Case TipoDeMercancia
      Case eTDM_NoAplica: enumTipoDeMercanciaToString = "No Aplica"
      Case eTDM_MateriaPrima: enumTipoDeMercanciaToString = "Materia Prima"
      Case eTDM_ProductoTerminado: enumTipoDeMercanciaToString = "Producto Terminado"
      Case Else
         enumTipoDeMercanciaToString = enumTipoDeMercanciaToString(0)
   End Select
End Function

Public Function strTipoDeMercanciaToNum(ByVal TipoDeMercancia As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeMercancia
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDM_NoAplica To eTDM_ProductoTerminado
      If UCase(TipoDeMercancia) = UCase(enumTipoDeMercanciaToString(mEnum)) Then
         strTipoDeMercanciaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeMercanciaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeMercancia & " no existe en tipo enumerado TipoDeMercancia"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeMercancia(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeMercanciaToString(enum_TipoDeMercancia.eTDM_NoAplica)
   End If
   varComboBox.List(nCount) = enumTipoDeMercanciaToString(enum_TipoDeMercancia.eTDM_NoAplica)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeMercanciaToString(enum_TipoDeMercancia.eTDM_MateriaPrima)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeMercanciaToString(enum_TipoDeMercancia.eTDM_ProductoTerminado)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDePersonaLibrosElectronicosToString(TipoDePersonaLibrosElectronicos As enum_TipoDePersonaLibrosElectronicos) As String
   Select Case TipoDePersonaLibrosElectronicos
      Case eTDPLE_NaturalDomiciliado: enumTipoDePersonaLibrosElectronicosToString = "Natural Domiciliado"
      Case eTDPLE_JuridicoDomiciliado: enumTipoDePersonaLibrosElectronicosToString = "Jurídico Domiciliado"
      Case eTDPLE_NaturalNoDomiciliado: enumTipoDePersonaLibrosElectronicosToString = "Natural No Domiciliado"
      Case eTDPLE_JuridicoNoDomiciliado: enumTipoDePersonaLibrosElectronicosToString = "Jurídico No Domiciliado"
      Case Else
         enumTipoDePersonaLibrosElectronicosToString = enumTipoDePersonaLibrosElectronicosToString(0)
   End Select
End Function

Public Function strTipoDePersonaLibrosElectronicosToNum(ByVal TipoDePersonaLibrosElectronicos As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDePersonaLibrosElectronicos
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDPLE_NaturalDomiciliado To eTDPLE_JuridicoNoDomiciliado
      If UCase(TipoDePersonaLibrosElectronicos) = UCase(enumTipoDePersonaLibrosElectronicosToString(mEnum)) Then
         strTipoDePersonaLibrosElectronicosToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDePersonaLibrosElectronicosToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDePersonaLibrosElectronicos & " no existe en tipo enumerado TipoDePersonaLibrosElectronicos"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDePersonaLibrosElectronicos(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDePersonaLibrosElectronicosToString(enum_TipoDePersonaLibrosElectronicos.eTDPLE_NaturalDomiciliado)
   End If
   varComboBox.List(nCount) = enumTipoDePersonaLibrosElectronicosToString(enum_TipoDePersonaLibrosElectronicos.eTDPLE_NaturalDomiciliado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDePersonaLibrosElectronicosToString(enum_TipoDePersonaLibrosElectronicos.eTDPLE_JuridicoDomiciliado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDePersonaLibrosElectronicosToString(enum_TipoDePersonaLibrosElectronicos.eTDPLE_NaturalNoDomiciliado)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDePersonaLibrosElectronicosToString(enum_TipoDePersonaLibrosElectronicos.eTDPLE_JuridicoNoDomiciliado)
   varComboBox.ListIndex = 1  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeCambioSunatToString(TipoDeCambioSunat As enum_TipoDeCambioSunat) As String
   Select Case TipoDeCambioSunat
      Case eTDCS_COMPRA: enumTipoDeCambioSunatToString = "Compra"
      Case eTDCS_VENTA: enumTipoDeCambioSunatToString = "Venta"
      Case Else
         enumTipoDeCambioSunatToString = enumTipoDeCambioSunatToString(0)
   End Select
End Function

Public Function strTipoDeCambioSunatToNum(ByVal TipoDeCambioSunat As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeCambioSunat
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDCS_COMPRA To eTDCS_VENTA
      If UCase(TipoDeCambioSunat) = UCase(enumTipoDeCambioSunatToString(mEnum)) Then
         strTipoDeCambioSunatToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeCambioSunatToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeCambioSunat & " no existe en tipo enumerado TipoDeCambioSunat"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoDeCambioSunat(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoDeCambioSunatToString(enum_TipoDeCambioSunat.eTDCS_COMPRA)
   End If
   varComboBox.List(nCount) = enumTipoDeCambioSunatToString(enum_TipoDeCambioSunat.eTDCS_COMPRA)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoDeCambioSunatToString(enum_TipoDeCambioSunat.eTDCS_VENTA)
   varComboBox.ListIndex = 1  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoDeMonedaToString(TipoDeMoneda As enum_TipoDeMoneda) As String
   Select Case TipoDeMoneda
      Case eTDMON_FISICA: enumTipoDeMonedaToString = "Física"
      Case eTDMON_CRIPTOACTIVO: enumTipoDeMonedaToString = "CriptoActivo"
      Case eTDMON_OTRAS: enumTipoDeMonedaToString = "Otras"
      Case Else
         enumTipoDeMonedaToString = enumTipoDeMonedaToString(0)
   End Select
End Function

Public Function strTipoDeMonedaToNum(ByVal TipoDeMoneda As String, Optional ByVal ShowMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeMoneda
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTDMON_FISICA To eTDMON_OTRAS
      If UCase(TipoDeMoneda) = UCase(enumTipoDeMonedaToString(mEnum)) Then
         strTipoDeMonedaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeMonedaToNum = 0
   If ShowMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoDeMoneda & " no existe en tipo enumerado TipoDeMoneda"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumTipoGeneradoPorNotaDeEntradaSalidaToString(TipoGeneradoPorNotaDeEntradaSalida As enum_TipoGeneradoPorNotaDeEntradaSalida) As String
   Select Case TipoGeneradoPorNotaDeEntradaSalida
      Case eTGPNDES_Usuario: enumTipoGeneradoPorNotaDeEntradaSalidaToString = "Usuario"
      Case eTGPNDES_OrdenDeProduccion: enumTipoGeneradoPorNotaDeEntradaSalidaToString = "Orden De Produccion"
      Case Else
         enumTipoGeneradoPorNotaDeEntradaSalidaToString = enumTipoGeneradoPorNotaDeEntradaSalidaToString(0)
   End Select
End Function

Public Function strTipoGeneradoPorNotaDeEntradaSalidaToNum(ByVal TipoGeneradoPorNotaDeEntradaSalida As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoGeneradoPorNotaDeEntradaSalida
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTGPNDES_Usuario To eTGPNDES_OrdenDeProduccion
      If UCase(TipoGeneradoPorNotaDeEntradaSalida) = UCase(enumTipoGeneradoPorNotaDeEntradaSalidaToString(mEnum)) Then
         strTipoGeneradoPorNotaDeEntradaSalidaToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoGeneradoPorNotaDeEntradaSalidaToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoGeneradoPorNotaDeEntradaSalida & " no existe en tipo enumerado TipoGeneradoPorNotaDeEntradaSalida"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoGeneradoPorNotaDeEntradaSalida(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoGeneradoPorNotaDeEntradaSalidaToString(enum_TipoGeneradoPorNotaDeEntradaSalida.eTGPNDES_Usuario)
   End If
   varComboBox.List(nCount) = enumTipoGeneradoPorNotaDeEntradaSalidaToString(enum_TipoGeneradoPorNotaDeEntradaSalida.eTGPNDES_Usuario)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoGeneradoPorNotaDeEntradaSalidaToString(enum_TipoGeneradoPorNotaDeEntradaSalida.eTGPNDES_OrdenDeProduccion)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

Public Function enumTipoNotaProduccionToString(TipoNotaProduccion As enum_TipoNotaProduccion) As String
   Select Case TipoNotaProduccion
      Case eTNP_NoAplica: enumTipoNotaProduccionToString = "No  Aplica"
      Case eTNP_Abrir: enumTipoNotaProduccionToString = "Abrir"
      Case eTNP_Cerrar: enumTipoNotaProduccionToString = "Cerrar"
      Case eTNP_Ajuste: enumTipoNotaProduccionToString = "Ajuste"
      Case Else
         enumTipoNotaProduccionToString = enumTipoNotaProduccionToString(0)
   End Select
End Function

Public Function strTipoNotaProduccionToNum(ByVal TipoNotaProduccion As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoNotaProduccion
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTNP_NoAplica To eTNP_Ajuste
      If UCase(TipoNotaProduccion) = UCase(enumTipoNotaProduccionToString(mEnum)) Then
         strTipoNotaProduccionToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoNotaProduccionToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & TipoNotaProduccion & " no existe en tipo enumerado TipoNotaProduccion"
   End If
h_EXIT:
   Exit Function
End Function

Public Sub FillComboBoxWithTipoNotaProduccion(ByRef varComboBox As Object, Optional action As AccionSobreRecord = Abrir)
   Dim nCount As Integer
   varComboBox.Clear
   nCount = 0
   If action = Buscar Then
      varComboBox.Text = gDefGen.getSearchAllComboBox()
      varComboBox.List(nCount) = gDefGen.getSearchAllComboBox()
      nCount = nCount + 1
   Else
      varComboBox.Text = enumTipoNotaProduccionToString(enum_TipoNotaProduccion.eTNP_NoAplica)
   End If
   varComboBox.List(nCount) = enumTipoNotaProduccionToString(enum_TipoNotaProduccion.eTNP_NoAplica)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoNotaProduccionToString(enum_TipoNotaProduccion.eTNP_Abrir)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoNotaProduccionToString(enum_TipoNotaProduccion.eTNP_Cerrar)
   nCount = nCount + 1
   varComboBox.List(nCount) = enumTipoNotaProduccionToString(enum_TipoNotaProduccion.eTNP_Ajuste)
   varComboBox.ListIndex = 0  ' Se selecciona el valor por defecto
   varComboBox.Width = gApi.sugestedWidthForComboBox(varComboBox)
End Sub

