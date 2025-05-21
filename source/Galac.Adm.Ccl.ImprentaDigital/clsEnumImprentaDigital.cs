using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.ImprentaDigital {    

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDocumentoImprentaDigital {
        [LibEnumDescription("Facturación")] Facturacion = 0,
        [LibEnumDescription("Retención IVA")] RetencionIVA,
        [LibEnumDescription("Retención ISLR")] RetencionISLR
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeTransaccionID {
        [LibEnumDescription("Factura")]
        Factura = 0,
        [LibEnumDescription("Giro")]
        Giro,
        [LibEnumDescription("Cheque Devuelto")]
        ChequeDevuelto,
        [LibEnumDescription("Nota De Crédito")]
        NotaDeCredito,
        [LibEnumDescription("Nota De Debito")]
        NotaDeDebito,
        [LibEnumDescription("Nota De Entrega")]
        NotaDeEntrega,
        [LibEnumDescription("No Asignado")]
        NoAsignado,
        [LibEnumDescription("Boleta De Venta")]
        BoletaDeVenta,
        [LibEnumDescription("Ticket Maquina Registradora")]
        TicketMaquinaRegistradora,
        [LibEnumDescription("Recibo Por Honorarios")]
        ReciboPorHonorarios,
        [LibEnumDescription("Liquidación De Compra")]
        LiquidacionDeCompra,
        [LibEnumDescription("Otros")]
        NotaDeDebitoCompFiscal,
        [LibEnumDescription("Nota De Crédito Comprobante Fiscal")]
        NotaDeCreditoCompFiscal,
        [LibEnumDescription("Guía de Remisión")]
        GuiaDeRemision
    }

    public enum eTipoDeProveedorDeLibrosFiscalesID {
        [LibEnumDescription("Con RIF")]
        ConRif = 0,
        [LibEnumDescription("Sin RIF")]
        SinRif,
        [LibEnumDescription("No Residenciado")]
        NoResidenciado,
        [LibEnumDescription("No Domiciliado")]
        NoDomiciliado
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusDocumentoCxP {
        [LibEnumDescription("Por Cancelar")]
        PorCancelar = 0,
        [LibEnumDescription("Cancelado")]
        Cancelado,
        [LibEnumDescription("Cheque Devuelto")]
        ChequeDevuelto,
        [LibEnumDescription("Abonado")]
        Abonado,
        [LibEnumDescription("Anulado")]
        Anulado,
        [LibEnumDescription("Refinanciado")]
        Refinanciado
    }

    #region Comandos Thefactory HKA

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComandosPostTheFactoryHKA {
        [LibEnumDescription("/api/Autenticacion")]
        Autenticacion = 0,
        [LibEnumDescription("/api/Emision")]
        Emision,
        [LibEnumDescription("/api/Anular")]
        Anular,
        [LibEnumDescription("/api/EstadoDocumento")]
        EstadoDocumento,
        [LibEnumDescription("/api/EstadoLote")]
        EstadoLote,
        [LibEnumDescription("/api/ListadoDocumentos")]
        ListadoDocumentos,
        [LibEnumDescription("/api/DescargaArchivo")]
        DescargaArchivo,
        [LibEnumDescription("/api/AplicarRetencion")]
        AplicarRetencion,
        [LibEnumDescription("/api/EnviarCorreo")]
        EnviarCorreo,
        [LibEnumDescription("/api/RastrearCorreo")]
        RastrearCorreo
    }

    #endregion Comandos Thefactory HKA

    #region Comandos Novus
    public enum eComandosPostNovus {
        [LibEnumDescription("/Autenticacion/v3")]
        Autenticacion = 0,
        [LibEnumDescription("/facturacion/v3")]
        Emision,
        [LibEnumDescription("/Anulacion/v3")]
        Anular,
        [LibEnumDescription("/email/v3")]
        Email,
        [LibEnumDescription("/facturacion/status")]
        EstadoDocumento
    }
    #endregion Comandos Novus

    #region Comandos Unidigital
    public enum eComandosPostUnidigital {
        [LibEnumDescription("/user/login")]
        Autenticacion = 0,
        [LibEnumDescription("/documents/createandapprove")]
        Emision,
        [LibEnumDescription("/documents/anulled")]
        Anular,
        [LibEnumDescription("/documents/notified")]
        Email,
        [LibEnumDescription("/documents/searchbynumberandserie")]
        EstadoDocumento,
        [LibEnumDescription("/documents?strongId=")]
        ObtenerNroControl
    }
    #endregion Comandos Unidigital
} //End of namespace namespace Galac.Saw.Ccl.ImprentaDigital
