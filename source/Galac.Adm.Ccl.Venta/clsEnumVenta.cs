using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusFactura {
        [LibEnumDescription("Emitida")]
        Emitida = 0,
        [LibEnumDescription("Anulada")]
        Anulada,
        [LibEnumDescription("Borrador")]
        Borrador
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormadePago {
        [LibEnumDescription("Contado")]
        Contado = 0,
        [LibEnumDescription("Crédito")]
        Credito
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeLaInicial {
        [LibEnumDescription("Por Monto")]
        PorMonto = 0,
        [LibEnumDescription("Por Porcentaje")]
        PorPorcentaje
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTalonario {
        [LibEnumDescription("Talonario 1")]
        Talonario1 = 0,
        [LibEnumDescription("Talonario 2")]
        Talonario2
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeVenta {
        [LibEnumDescription("Interna")]
        Interna = 0,
        [LibEnumDescription("Exportación")]
        Exportacion,
        [LibEnumDescription("Sin  Derecho a  Crédito  Fiscal")]
        SinDerechoaCreditoFiscal,
        [LibEnumDescription("A  Contribuyente")]
        AContribuyente,
        [LibEnumDescription("A  No  Contribuyente")]
        ANoContribuyente
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeFormaDeCobro {
        [LibEnumDescription("Efectivo")]
        Efectivo = 0,
        [LibEnumDescription("Tarjeta Débito")]
        TarjetaDebito,
        [LibEnumDescription("Tarjeta Crédito")]
        TarjetaCredito,
        [LibEnumDescription("Cheque")]
        Cheque,
        [LibEnumDescription("Otros")]
        Otros
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeCxC {
        [LibEnumDescription("Factura")]
        Factura = 0,
        [LibEnumDescription("Cheque Devuelto")]
        ChequeDevuelto,
        [LibEnumDescription("Nota de Crédito")]
        NotaDeCredito,
        [LibEnumDescription("Nota De Débito")]
        NotaDeDebito,
        [LibEnumDescription("Nota de Entrega")]
        NotaEntrega,
        [LibEnumDescription("Giro")]
        Giro,
        [LibEnumDescription("No Asignado")]
        NoAsignado
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeTransaccionDeLibrosFiscales {
        [LibEnumDescription("Registro")]
        Registro = 0,
        [LibEnumDescription("Complemento")]
        Complemento,
        [LibEnumDescription("Anulación")]
        Anulacion,
        [LibEnumDescription("Ajuste")]
        Ajuste
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

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusCobranza {
        [LibEnumDescription("Vigente")]
        Vigente = 0,
        [LibEnumDescription("Anulada")]
        Anulada
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
    public enum eTipoDeTarjeta {
        [LibEnumDescription("Visa")]
        Visa = 0,
        [LibEnumDescription("Master")]
        Master,
        [LibEnumDescription("American")]
        American,
        [LibEnumDescription("Débito")]
        Debito
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eOrigenFacturacionOManual {
        [LibEnumDescription("Factura")]
        Factura = 0,
        [LibEnumDescription("Manual")]
        Manual,
        [LibEnumDescription("Sistema 10")]
        Sistema10
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusRetencionIVACobranza {
        [LibEnumDescription("No Aplica")]
        NoAplica = 0,
        [LibEnumDescription("Pendiente por Distribuir")]
        PendientePorDistribuir,
        [LibEnumDescription("Distribuido")]
        Distribuido
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeDocumentoCobranza {
        [LibEnumDescription("Cobranza De Factura")]
        CobranzaDeFactura = 0,
        [LibEnumDescription("Cobranza Por Aplicacion De Retencion")]
        CobranzaPorAplicacionDeRetencion
    }

    
    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeCobro {
        [LibEnumDescription("Efectivo", Index = 0)]
        [LibEnumDescription("00001", Index = 1)]
        Efectivo = 0,
        [LibEnumDescription("Tarjeta", Index = 0)]
        [LibEnumDescription("00002", Index = 1)]
        Tarjeta,
        [LibEnumDescription("Cheque", Index = 0)]
        [LibEnumDescription("00003", Index = 1)]
        Cheque,
        [LibEnumDescription("Depósito", Index = 0)]
        [LibEnumDescription("00004", Index = 1)]
        Deposito,
        [LibEnumDescription("Anticipo", Index = 0)]
        [LibEnumDescription("00005", Index = 1)]
        Anticipo,
        [LibEnumDescription("Transferencia", Index = 0)]
        [LibEnumDescription("00006", Index = 1)]
        Transferencia,
        [LibEnumDescription("Vuelto Efectivo", Index = 0)]
        [LibEnumDescription("00007", Index = 1)]
        VueltoEfectivo,
        [LibEnumDescription("Vuelto Pago Móvil", Index = 0)]
        [LibEnumDescription("00008", Index = 1)]
        VueltoC2P,
        [LibEnumDescription("Tarjeta Medios Electrónicos")]
        [LibEnumDescription("00009", Index = 1)]
        TarjetaMS,
        [LibEnumDescription("Zelle")]
        [LibEnumDescription("00010", Index = 1)]
        Zelle,
        [LibEnumDescription("P2C")]
        [LibEnumDescription("00011", Index = 1)]
        PagoMovil,
        [LibEnumDescription("Transferencia Medios Electrónicos")]
        [LibEnumDescription("00012", Index = 1)]
        TransferenciaMS,
        [LibEnumDescription("C2P")]
        [LibEnumDescription("00013", Index = 1)]
        C2P
    }    

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFiltrarCobranzasPor {
        [LibEnumDescription("Cobrador")]
        Cobrador = 0,
        [LibEnumDescription("Cliente")]
        Cliente,
        [LibEnumDescription("Cuenta Bancaria")]
        CuentaBancaria,
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeCalculoComision {
        [LibEnumDescription("Por Ventas")]
        PorVentas = 0,
        [LibEnumDescription("Por Cobranzas")]
        PorCobranzas
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComoAplicaOtrosCargosDeFactura {
        [LibEnumDescription("Suma")]
        Suma = 0,
        [LibEnumDescription("Resta")]
        Resta,
        [LibEnumDescription("No Aplica - Es Informativo")]
        NoAplica_EsInformativo
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eClientesOrdenadosPor {
        [LibEnumDescription("Por Código")]
        PorCodigo = 0,
        [LibEnumDescription("Por Nombre")]
        PorNombre
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusContrato {
        [LibEnumDescription("Vigente")]
        Vigente = 0,
        [LibEnumDescription("Desactivado")]
        Desactivado
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eDuracionDelContrato {
        [LibEnumDescription("Duración Fija")]
        DuracionFija = 0,
        [LibEnumDescription("Duración Indefinida")]
        DuracionIndefinida
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum ePeriodicidadContratos {
        [LibEnumDescription("Mensual")]
        Mensual = 0,
        [LibEnumDescription("Bimestral")]
        Bimestral,
        [LibEnumDescription("Trimestral")]
        Trimestral,
        [LibEnumDescription("Semestral")]
        Semestral,
        [LibEnumDescription("Anual")]
        Anual,
        [LibEnumDescription("Cuota Única")]
        CuotaUnica
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum ePeriodoDeAplicacion {
        [LibEnumDescription("El del Contrato")]
        EldelContrato = 0,
        [LibEnumDescription("Especial Indicar")]
        EspecialIndicar
    }

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eValorDelRenglon {
        [LibEnumDescription("Valor en Archivo Artículos")]
        ValorenArchivoArticulos = 0,
        [LibEnumDescription("Indicar El Valor")]
        IndicarElValor
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eFacturaGeneradaPor {
        [LibEnumDescription("Usuario")]
        Usuario = 0,
        [LibEnumDescription("Cotizacion")]
        Cotizacion,
        [LibEnumDescription("Galac 360")]
        Galac360,
        [LibEnumDescription("Ajuste IGTF")]
        AjusteIGTF
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eIdFiscalPM {
        [LibEnumDescription("V")] V = 0,
        [LibEnumDescription("J")] J,
        [LibEnumDescription("E")] E,
        [LibEnumDescription("G")] G,
        [LibEnumDescription("P")] P
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eCodigoCel {
        [LibEnumDescription("0412")] Cod_0412 = 0,
        [LibEnumDescription("0414")] Cod_0414,
        [LibEnumDescription("0424")] Cod_0424,
        [LibEnumDescription("0416")] Cod_0416,
        [LibEnumDescription("0426")] Cod_0426
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBancoPM {
        [LibEnumDescription("No Aplica")] NoAplica = 0,
        [LibEnumDescription("0102 Banco de Venezuela S.A. Banco Universal")] Bco_0102,
        [LibEnumDescription("0104 Banco Venezolano de Crédito S.A. Banco Universal")] Bco_0104,
        [LibEnumDescription("0105 Banco Mercantil C.A. Banco Universal")] Bco_0105,
        [LibEnumDescription("0108 Banco Provincial S.A. Banco Universal")] Bco_0108,
        [LibEnumDescription("0114 Banco del Caribe C.A. Banco Universal")] Bco_0114,
        [LibEnumDescription("0115 Banco Exterior C.A. Banco Universal")] Bco_0115,
        [LibEnumDescription("0128 Banco Caroní C.A. Banco Universal")] Bco_0128,
        [LibEnumDescription("0134 Banesco Banco Universal C.A.")] Bco_0134,
        [LibEnumDescription("0137 Banco Sofitasa Banco Universal C.A.")] Bco_0137,
        [LibEnumDescription("0138 Banco Plaza Banco universal")] Bco_0138,
        [LibEnumDescription("0146 Banco de la Gente Emprendedora C.A.")] Bco_0146,
        [LibEnumDescription("0151 Banco Fondo Común C.A Banco Universal")] Bco_0151,
        [LibEnumDescription("0156 100% Banco Banco Comercial C.A")] Bco_0156,
        [LibEnumDescription("0157 DelSur Banco Universal C.A.")] Bco_0157,
        [LibEnumDescription("0163 Banco del Tesoro C.A. Banco Universal")] Bco_0163,
        [LibEnumDescription("0168 Bancrecer S.A. Banco Microfinanciero")] Bco_0168,
        [LibEnumDescription("0169 Mi Banco Banco Microfinanciero C.A.")] Bco_0169,
        [LibEnumDescription("0171 Banco Activo C.A. Banco Universal")] Bco_0171,
        [LibEnumDescription("0172 Bancamiga Banco Universal C.A.")] Bco_0172,
        [LibEnumDescription("0174 Banplus Banco Universal C.A.")] Bco_0174,
        [LibEnumDescription("0175 Banco Bicentenario del Pueblo Banco Universal C.A.")] Bco_0175,
        [LibEnumDescription("0177 Banco de la Fuerza Armada Nacional Bolivariana B.U.")] Bco_0177,
        [LibEnumDescription("0191 Banco Nacional de Crédito C.A. Banco Universal")] Bco_0191
    }

} //End of namespace namespace Galac.Saw.Ccl.Venta
