using LibGalac.Aos.Base;
using System.ComponentModel;

namespace Galac.Saw.Lib {

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eValidaPorTipoDeDocumento {
        [LibEnumDescription("Factura")] Factura = 0,
        [LibEnumDescription("Boleta")] Boleta,
        [LibEnumDescription("Ambos")] Ambos,
        [LibEnumDescription("Guía de Remisión")] GuiaDeRemision
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eMonedaParaImpresion {
        [LibEnumDescription("En Bolívares")] EnBolivares = 0,
        [LibEnumDescription("En Soles")] EnSoles,
        [LibEnumDescription("En Moneda Original")] EnMonedaOriginal
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eMonedaPresentacionDeReporte {
        [LibEnumDescription("En Moneda Local")] EnMonedaLocal = 0,
        [LibEnumDescription("En Divisa")] EnDivisa,
        [LibEnumDescription("En Ambas Monedas")] EnAmbasMonedas
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTasaDeCambioParaImpresion {
        [LibEnumDescription("Original")] Original = 0,
        [LibEnumDescription("Del Día")] DelDia
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eSystemModules {
        [LibEnumDescription("Menu G")] MenuG = 0,
        [LibEnumDescription("Alicuota IVA")] AlicuotaIVA,
        [LibEnumDescription("Almacen")] Almacen,
        [LibEnumDescription("Anticipo")] Anticipo,
        [LibEnumDescription("Anticipo Cobrado")] AnticipoCobrado,
        [LibEnumDescription("Anticipo Pagado")] AnticipoPagado,
        [LibEnumDescription("Articulo Inventario")] ArticuloInventario,
        [LibEnumDescription("Autorizacion")] Autorizacion,
        [LibEnumDescription("Banco")] Banco,
        [LibEnumDescription("Caja")] Caja,
        [LibEnumDescription("Caja Apertura")] CajaApertura,
        [LibEnumDescription("Cambiar Dirección De Despacho")] CambiarDireccionDeDespacho,
        [LibEnumDescription("Campos Definibles Cotización")] CamposDefCotizacion,
        [LibEnumDescription("Campos Definibles Factura")] CamposDefFactura,
        [LibEnumDescription("Cambio a Moneda Local")] CambioaMonedaLocal,
        [LibEnumDescription("Campos Moneda Extranjera")] CamposMonedaExtranjera,
        [LibEnumDescription("Categoria")] Categoria,
        [LibEnumDescription("Categoria Cliente")] CategoriaCliente,
        [LibEnumDescription("Categoria Proveedor")] CategoriaProveedor,
        [LibEnumDescription("Cheque De Movimiento Bancario")] ChequeDeMovBancario,
        [LibEnumDescription("CIIU")] CIIU,
        [LibEnumDescription("Ciudad")] Ciudad,
        [LibEnumDescription("Clave Super Utilitario")] ClaveSuperUtilitario,
        [LibEnumDescription("Cliente")] Cliente,
        [LibEnumDescription("Cobranza")] Cobranza,
        [LibEnumDescription("Color")] Color,
        [LibEnumDescription("Comisión Cobranza X Vcto")] ComisionCobranzaXVcto,
        [LibEnumDescription("Compania")] Compania,
        [LibEnumDescription("Compra")] Compra,
        [LibEnumDescription("Concepto Bancario")] ConceptoBancario,
        [LibEnumDescription("Conciliación")] Conciliacion,
        [LibEnumDescription("Conteo Físico")] ConteoFisico,
        [LibEnumDescription("Contrato")] Contrato,
        [LibEnumDescription("Corrección De Fechas")] CorreccionDeFechas,
        [LibEnumDescription("Cotización")] Cotizacion,
        [LibEnumDescription("Cuenta Bancaria")] CuentaBancaria,
        [LibEnumDescription("Curso")] Curso,
        [LibEnumDescription("CxC")] CxC,
        [LibEnumDescription("CxP")] CxP,
        [LibEnumDescription("Despacho")] Despacho,
        [LibEnumDescription("Día De Declaracion Del IVA")] DiaDeDeclaracionDelIVA,
        [LibEnumDescription("Direccion De Despacho")] DireccionDeDespacho,
        [LibEnumDescription("Documento Cobrado")] DocumentoCobrado,
        [LibEnumDescription("Documento Pagado")] DocumentoPagado,
        [LibEnumDescription("Existencia Por Almacen")] ExistenciaPorAlmacen,
        [LibEnumDescription("Existencia Por Grupo")] ExistenciaPorGrupo,
        [LibEnumDescription("Factura")] Factura,
        [LibEnumDescription("Facturas Vendedor")] FacturasVendedor,
        [LibEnumDescription("Fax Masivo")] FaxMasivo,
        [LibEnumDescription("Forma Del Cobro")] FormaDelCobro,
        [LibEnumDescription("Generación De Contratos")] GeneracionDeContratos,
        [LibEnumDescription("Grupo Talla Color")] GrupoTallaColor,
        [LibEnumDescription("Linea De Producto")] LineaDeProducto,
        [LibEnumDescription("Llamada")] Llamada,
        [LibEnumDescription("Lote Adm")] LoteAdm,
        [LibEnumDescription("Mail Fax")] MailFax,
        [LibEnumDescription("Maquina Fiscal")] MaquinaFiscal,
        [LibEnumDescription("Marca Vehículo")] MarcaVehiculo,
        [LibEnumDescription("Modelo Vehículo")] ModeloVehiculo,
        [LibEnumDescription("Moneda")] Moneda,
        [LibEnumDescription("Moneda Local")] MonedaLocal,
        [LibEnumDescription("Movimiento Bancario")] MovimientoBancario,
        [LibEnumDescription("Nota De Entrada Salida")] NotaDeEntradaSalida,
        [LibEnumDescription("Nota Final")] NotaFinal,
        [LibEnumDescription("OPFalsoRetencion")] OPFalsoRetencion,
        [LibEnumDescription("Orden De Compra")] OrdenDeCompra,
        [LibEnumDescription("Orden De Servicio")] OrdenDeServicio,
        [LibEnumDescription("Otros Cargos De Factura")] OtrosCargosDeFactura,
        [LibEnumDescription("Pago")] Pago,
        [LibEnumDescription("País")] Pais,
        [LibEnumDescription("Parametros")] Parametros,
        [LibEnumDescription("Parametro sCompania")] ParametrosCompania,
        [LibEnumDescription("Participante")] Participante,
        [LibEnumDescription("Planilla Forma00030")] PlanillaForma00030,
        [LibEnumDescription("Preguntas Más Comunes")] PreguntasMasComunes,
        [LibEnumDescription("Producto Compuesto")] ProductoCompuesto,
        [LibEnumDescription("PropAnalisisVenc")] PropAnalisisVenc,
        [LibEnumDescription("Proveedor")] Proveedor,
        [LibEnumDescription("Reglas De Contabilización")] ReglasDeContabilizacion,
        [LibEnumDescription("Renglon Art Compuesto Cotización")] RenglonArtCompuestoCot,
        [LibEnumDescription("Renglon Art Compuesto Factura")] RenglonArtCompuestoFac,
        [LibEnumDescription("Renglon Cobro De Factura")] RenglonCobroDeFactura,
        [LibEnumDescription("Renglon Comisiones De Vendedor")] RenglonComisionesDeVendedor,
        [LibEnumDescription("RenglonC ompra")] RenglonCompra,
        [LibEnumDescription("Renglon Compra X Serial")] RenglonCompraXSerial,
        [LibEnumDescription("Renglon Conteo Físico")] RenglonConteoFisico,
        [LibEnumDescription("Renglon Contrato")] RenglonContrato,
        [LibEnumDescription("Renglon Cotización")] RenglonCotizacion,
        [LibEnumDescription("Renglon Existencia Almacen")] RenglonExistenciaAlmacen,
        [LibEnumDescription("Renglon Factura")] RenglonFactura,
        [LibEnumDescription("Renglon Grupo Color")] RenglonGrupoColor,
        [LibEnumDescription("Renglon Grupo Talla")] RenglonGrupoTalla,
        [LibEnumDescription("Renglon NotaES")] RenglonNotaES,
        [LibEnumDescription("Renglon Otros Cargos De Cotización")] RenglonOtrosCargosDeCotizacion,
        [LibEnumDescription("Renglón Otros Cargos De Factura")] RenglonOtrosCargosDeFactura,
        [LibEnumDescription("Renglon Transferencia")] RenglonTransferencia,
        [LibEnumDescription("Respaldar Restaurar")] RespaldarRestaurar,
        [LibEnumDescription("Resumen Diario Ventas")] ResumenDiarioVentas,
        [LibEnumDescription("Ret Documento Pagado")] RetDocumentoPagado,
        [LibEnumDescription("Retiros A Cuenta")] RetirosACuenta,
        [LibEnumDescription("Retención Pago")] RetPago,
        [LibEnumDescription("Revisión De Data")] RevisionDeData,
        [LibEnumDescription("Sector De Negocio")] SectorDeNegocio,
        [LibEnumDescription("Tabla Retención")] TablaRetencion,
        [LibEnumDescription("Talla")] Talla,
        [LibEnumDescription("Talonario")] Talonario,
        [LibEnumDescription("TipoDeCurso")] TipoDeCurso,
        [LibEnumDescription("TipoProveedor")] TipoProveedor,
        [LibEnumDescription("Transferencia")] Transferencia,
        [LibEnumDescription("UnidadDeVenta")] UnidadDeVenta,
        [LibEnumDescription("UrbanizacionZP")] UrbanizacionZP,
        [LibEnumDescription("Usuario")] Usuario,
        [LibEnumDescription("Vendedor")] Vendedor,
        [LibEnumDescription("VersionPrograma")] VersionPrograma,
        [LibEnumDescription("Visita")] Visita,
        [LibEnumDescription("Zona de Cobranza")] ZonaCobranza,
        [LibEnumDescription("Gestión De Cobranza")] GestionDeCobranza,
        [LibEnumDescription("Comprobante Retención IVA")] ComprobanteRetencionIVA,
        [LibEnumDescription("Debito Bancario")] DebitoBancario,
        [LibEnumDescription("Servicios Al Realizar Visita")] ServiciosAlRealizarVisita,
        [LibEnumDescription("Planilla Forma00030 Nueva")] PlanillaForma00030Nueva,
        [LibEnumDescription("PDT")] PDT,
        [LibEnumDescription("Anticipo Adelanto")] AnticipoAdelanto,
        [LibEnumDescription("Rendición")] Rendicion,
        [LibEnumDescription("Detalle De Rendición")] DetalleDeRendicion,
        [LibEnumDescription("CajaChica")] CajaChica,
        [LibEnumDescription("Adelanto")] Adelanto,
        [LibEnumDescription("Detracciones")] Detracciones,
        [LibEnumDescription("Tipo De Documento Ley")] TipoDeDocumentoLey,
        [LibEnumDescription("Vehiculo")] Vehiculo,
        [LibEnumDescription("Control de Despacho")] ControlDespacho,
        [LibEnumDescription("Detalle De Control de Despacho")] DetalleDeControlDespacho,
        [LibEnumDescription("Planilla Dec")] PlanillaDec,
        [LibEnumDescription("Plantilla RET")] PlantillaRET,
        [LibEnumDescription("Valor UT")] ValorUT,
        [LibEnumDescription("Tarifa N2")] TarifaN2,
        [LibEnumDescription("ARCV")] ARCV,
        [LibEnumDescription("Relación Anual")] RelacionAnual,
        [LibEnumDescription("Renglon ARCV")] RenglonARCV,
        [LibEnumDescription("Conversion Contabilidad")] ConversionContabilidad,
        [LibEnumDescription("Contabilizar")] Contabilizar,
        [LibEnumDescription("Balances")] Balances,
        [LibEnumDescription("Informes")] Informes,
        [LibEnumDescription("Importar Exportar")] ImportarExportar,
        [LibEnumDescription("Cerrar Periodo")] CerrarPeriodo,
        [LibEnumDescription("Cierre Costo Periodo")] CierreCostoPeriodo,
        [LibEnumDescription("Detalle De Conciliación")] DetalleDeConciliacion,
        [LibEnumDescription("Inteligencia De Negocio")] InteligenciaDeNegocio,
        [LibEnumDescription("Informes De Vendedores")] InformesVendedores,
        [LibEnumDescription("Beneficiario")] Beneficiario,
        [LibEnumDescription("Solicitudes De Pago")] SolicitudesDePago,
        [LibEnumDescription("Renglón Solicitudes De Pago")] RenglonSolicitudesDePago,
        [LibEnumDescription("Integracion")] Integracion,
        [LibEnumDescription("Municipio")] Municipio,
        [LibEnumDescription("Municipio Ciudad")] MunicipioCiudad,
        [LibEnumDescription("Clasificador de Actividad Economica")] ClasificadorActividadEconomica,
        [LibEnumDescription("Impuesto Municipal Ret")] ImpuestoMunicipalRetNavigator,
        [LibEnumDescription("Renglon Impuesto Municipal")] RenglonImpuestoMunicipalRetNav,
        [LibEnumDescription("Nota De Entrega")] NotaDeEntrega,
        [LibEnumDescription("Guia De Remision")] GuiaDeRemision,
        [LibEnumDescription("Ubigeo")] Ubigeo,
        [LibEnumDescription("Provincia")] Provincia,
        [LibEnumDescription("Departamento")] Departamento,
        [LibEnumDescription("Motivo de Traslado")] MotivoTraslado,
        [LibEnumDescription("Conductor")] Conductor,
        [LibEnumDescription("Detalle Guia De Remisión")] DetalleGuiaDeRemision,
        [LibEnumDescription("Compra Detalle Gasto")] CompraDetalleGasto,
        [LibEnumDescription("Carga Inicial")] CargaInicial,
        [LibEnumDescription("ExC")] ExC,
        [LibEnumDescription("DetalleExC")] DetalleExC,
        [LibEnumDescription("Orden De Compra en Articulo Inventario")] OrdenDeCompraDetalleArticuloInventario,
        [LibEnumDescription("Condiciones De Pago")] CondicionesDePago
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeInforme {
        [LibEnumDescription("Detallado")] Detallado = 0,
        [LibEnumDescription("Resumido")] Resumido
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum ePrecioDeLosArticulos {
        [LibEnumDescription("Precio con IVA")] PrecioConIva = 0,
        [LibEnumDescription("Precio sin IVA")] PrecioSinIva
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeNivelDePrecio {
        [LibEnumDescription("Por Usuario")] PorUsuario = 0,
        [LibEnumDescription("Por Cliente")] PorCliente
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eInformeAgruparPor {
        [LibEnumDescription("No agrupar")] NoAgurpar = 0,
        [LibEnumDescription("Sector de Negocio")] SectorDeNegocio,
        [LibEnumDescription("Zona de Cobranza")] ZonaDeCobranza
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eInformeStatusCXC_CXP {
        [LibEnumDescription("Por Cancelar")] PorCancelar = 0,
        [LibEnumDescription("Cancelado")] Cancelado,
        [LibEnumDescription("Cheque Devuelto")] ChequeDevuelto,
        [LibEnumDescription("Abonado")] Abonado,
        [LibEnumDescription("Anulado")] Anulado,
        [LibEnumDescription("Refinanciado")] Refinanciado,
        [LibEnumDescription("Todos")] Todos
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eMonedaDelInformeMM {
        [LibEnumDescription("En Bolívares")] EnBolivares = 0,
        [LibEnumDescription("En Moneda Original")] EnMonedaOriginal,
        [LibEnumDescription("Bolívares expresados en...")] BolivaresExpresadosEnEnDivisa
    }

    public enum eComprobanteGeneradoPorVBSaw {
        eCG_USUARIO = 0,
        eCG_APERTURA,
        eCG_DEPRECIACION,
        eCG_CIERRE_CUENTAS_RESULTADO,
        eCG_CIERRE_INVENTARIO,
        eCG_ADMINISTRATIVO,
        eCG_IMPORTADO,
        eCG_ACTIVOFIJO,
        eCG_RESERVA,
        //'Contabilizacion Automática
        eCG_CXC,
        eCG_FACTURA,
        eCG_CXP,
        eCG_COBRANZA,
        eCG_PAGOS,
        eCG_MOVIMIENTO_BANCARIO,
        eCG_RETIRO_ACTIVOFIJO,
        eCG_CONCILIACION,
        eCG_RESUMEN_DIARIO_VENTAS,
        eCG_ANTICIPO,
        //'Reconversion Monetaria
        eCG_RECONVERSION,
        //'Contabilizacion Automática
        //'inventario'
        eCG_INVENTARIO,
        //'Contabilizacion Automática
        eCG_ANULACION_COBRANZA,
        eCG_ANULACION_PAGO,
        eCG_PAGO_SUELDOS,
        eCG_ANULACION_PAGO_SUELDOS,
        eCG_REVALORIZACION_ACTIVO,
        eCG_DEPRECIACION_REVALORIZACION,
        eCG_ANULACION_MOVIMIENTO_BANCARIO,
        eCG_RENDICION,
        eCG_REPOSICION,
        eCG_ANULACION_REPOSICION,
        eCG_TRANSFERENCIA_ENTRE_CUENTAS_BANCARIA,
        eCG_ORDEN_DE_PRODUCCION,
        eCG_GANANCIA_PERDIDA_CAMBIARIA = 80
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eClientesOrdenadosPor {
        [LibEnumDescription("Por Código")] PorCodigo = 0,
        [LibEnumDescription("Por Nombre")] PorNombre
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusCXC {
        [LibEnumDescription("Por Cancelar", Index = 0)]
        [LibEnumDescription("P/C", Index = 1)]
        PORCANCELAR = 0,
        [LibEnumDescription("Cancelado", Index = 0)]
        [LibEnumDescription("CAN", Index = 1)]
        CANCELADO,
        [LibEnumDescription("Cheque Devuelto", Index = 0)]
        [LibEnumDescription("C/D", Index = 1)]
        CHEQUEDEVUELTO,
        [LibEnumDescription("Abonado", Index = 0)]
        [LibEnumDescription("ABO", Index = 1)]
        ABONADO,
        [LibEnumDescription("Anulado", Index = 0)]
        [LibEnumDescription("ANU", Index = 1)]
        ANULADO,
        [LibEnumDescription("Refinanciado", Index = 0)]
        [LibEnumDescription("REF", Index = 1)]
        REFINANCIADO
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusCobranza {
        [LibEnumDescription("Vigente")]
        Vigente = 0,
        [LibEnumDescription("Anulada")]
        Anulada
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeDocumentoCobranza {
        [LibEnumDescription("Cobranza De Factura")]
        CobranzaDeFactura = 0,
        [LibEnumDescription("Cobranza Por Aplicacion De Retencion")]
        CobranzaPorAplicacionDeRetencion
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeTransaccion {
        [LibEnumDescription("FACTURA")]
        FACTURA = 0,
        [LibEnumDescription("GIRO")]
        GIRO,
        [LibEnumDescription("CHEQUE DEVUELTO")]
        CHEQUEDEVUELTO,
        [LibEnumDescription("NOTA DE CREDITO")]
        NOTADECREDITO,
        [LibEnumDescription("NOTA DE DEBITO")]
        NOTADEDEBITO,
        [LibEnumDescription("NOTA DE ENTREGA")]
        NOTAENTREGA,
        [LibEnumDescription("NO ASIGNADO")]
        NOASIGNADO,
        [LibEnumDescription("BOLETA DE VENTA")]
        BOLETADEVENTA,
        [LibEnumDescription("TICKET MAQUINA REGISTRADORA")]
        TICKETMAQUINAREGISTRADORA,
        [LibEnumDescription("RECIBO POR HONORARIOS")]
        RECIBOPORHONORARIOS,
        [LibEnumDescription("LIQUIDACION DE COMPRA")]
        LIQUIDACIONDECOMPRA,
        [LibEnumDescription("OTROS")]
        OTROS,
        [LibEnumDescription("NOTA DE CREDITO DE COMPROBANTE FISCAL")]
        NOTADECREDITOCOMPROBANTEFISCAL
    }
}
