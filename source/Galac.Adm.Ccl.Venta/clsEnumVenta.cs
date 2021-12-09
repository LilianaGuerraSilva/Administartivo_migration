using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusFactura {
        [LibEnumDescription("Emitida")]
        Emitida = 0,
        [LibEnumDescription("Anulada")]
        Anulada,
        [LibEnumDescription("Borrador")]
        Borrador
    }

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormadePago {
        [LibEnumDescription("Contado")]
        Contado = 0,
        [LibEnumDescription("Crédito")]
        Credito
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeLaInicial {
        [LibEnumDescription("Por Monto")]
        PorMonto = 0,
        [LibEnumDescription("Por Porcentaje")]
        PorPorcentaje
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTalonario {
        [LibEnumDescription("Talonario 1")]
        Talonario1 = 0,
        [LibEnumDescription("Talonario 2")]
        Talonario2
    }
	
    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeVenta {
        [LibEnumDescription("Interna")]
        Interna = 0,
        [LibEnumDescription("Exportacion")]
        Exportacion,
        [LibEnumDescription("Sin  Derecho a  Crédito  Fiscal")]
        SinDerechoaCreditoFiscal,
        [LibEnumDescription("A  Contribuyente")]
        AContribuyente,
        [LibEnumDescription("A  No  Contribuyente")]
        ANoContribuyente
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
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

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
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

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
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

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
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

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusCobranza {
        [LibEnumDescription("Vigente")]
        Vigente = 0,
        [LibEnumDescription("Anulada")]
        Anulada
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
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


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
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

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eOrigenFacturacionOManual {
        [LibEnumDescription("Factura")]
        Factura = 0,
        [LibEnumDescription("Manual")]
        Manual,
        [LibEnumDescription("Sistema 10")]
        Sistema10
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusRetencionIVACobranza {
        [LibEnumDescription("No Aplica")]
        NoAplica = 0,
        [LibEnumDescription("Pendiente por Distribuir")]
        PendientePorDistribuir,
        [LibEnumDescription("Distribuido")]
        Distribuido
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeDocumentoCobranza {
        [LibEnumDescription("Cobranza De Factura")]
        CobranzaDeFactura = 0,
        [LibEnumDescription("Cobranza Por Aplicacion De Retencion")]
        CobranzaPorAplicacionDeRetencion
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeCobro {
        [LibEnumDescription("Efectivo",Index = 0)]
        [LibEnumDescription("00001",Index = 1)]
        Efectivo = 0,
        [LibEnumDescription("Tarjeta",Index = 0)]
        [LibEnumDescription("00002",Index = 1)]
        Tarjeta,
        [LibEnumDescription("Cheque",Index = 0)]
        [LibEnumDescription("00003",Index = 1)]
        Cheque,
        [LibEnumDescription("Depósito",Index = 0)]
        [LibEnumDescription("00004",Index = 1)]
        Deposito,
        [LibEnumDescription("Anticipo",Index = 0)]
        [LibEnumDescription("00005",Index = 1)]
        Anticipo
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFiltrarCobranzasPor {
        [LibEnumDescription("Cobrador")]
        Cobrador = 0,
        [LibEnumDescription("Cliente")]
        Cliente,
        [LibEnumDescription("Cuenta Bancaria")]
        CuentaBancaria,
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeCalculoComision {
        [LibEnumDescription("Por Ventas")]
        PorVentas = 0,
        [LibEnumDescription("Por Cobranzas")]
        PorCobranzas
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComoAplicaOtrosCargosDeFactura {
        [LibEnumDescription("Suma")]
        Suma = 0,
        [LibEnumDescription("Resta")]
        Resta,
        [LibEnumDescription("No Aplica - Es Informativo")]
        NoAplica_EsInformativo
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eClientesOrdenadosPor {
        [LibEnumDescription("Por Código")]
        PorCodigo = 0, 
        [LibEnumDescription("Por Nombre")]
        PorNombre
	}

} //End of namespace namespace Galac.Saw.Ccl.Venta
