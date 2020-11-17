using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionCompras {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDocumentoIdentificacion {
        [LibEnumDescription("RUC")]
        RUC = 0, 
        [LibEnumDescription("DNI")]
        DNI, 
        [LibEnumDescription("Carnet de Extranjería")]
        CarnetdeExtranjeria, 
        [LibEnumDescription("Pasaporte")]
        Pasaporte, 
        [LibEnumDescription("Otros Tipos de Documentos")]
        OtrosTiposdeDocumentos
	}

        [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeProveedorDeLibrosFiscales {
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
        public enum ePorcentajeDeRetencionDeIVA {
            [LibEnumDescription("0")]
            por0 = 0,
            [LibEnumDescription("75")]
            por75,
            [LibEnumDescription("100")]
            por100
        }	

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoCompra {
        [LibEnumDescription("Nacional")]
        Nacional = 0,
        [LibEnumDescription("Importación")]
        Importacion
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoOrdenDeCompra {
        [LibEnumDescription("Nota de  Entrega")]
        NotadeEntrega = 0,
        [LibEnumDescription("Factura")]
        Factura
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeDistribucion {
        [LibEnumDescription("Ninguno")]
        Ninguno = 0,
        [LibEnumDescription("Manual  Por  Monto")]
        ManualPorMonto,
        [LibEnumDescription("Manual  Por  Porcentaje")]
        ManualPorPorcentaje,
        [LibEnumDescription("Automatica")]
        Automatica
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusCompra {
        [LibEnumDescription("Vigente")]
        Vigente = 0,
        [LibEnumDescription("Anulada")]
        Anulada
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusDeOrdenDeCompra {
        [LibEnumDescription("Completamente Procesada")]
        CompletamenteProcesada = 0,
        [LibEnumDescription("Parcialmente Procesada")]
        ParcialmenteProcesada,
        [LibEnumDescription("Sin Procesar")]
        SinProcesar
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


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeCosto {
        [LibEnumDescription("Flete  Internacional")]
        FleteInternacional = 0, 
        [LibEnumDescription("Seguro")]
        Seguro, 
        [LibEnumDescription("Nacionalizacion")]
        Nacionalizacion, 
        [LibEnumDescription("Otro")]
        Otro
	}
  
    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDeTransaccion {
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
	
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDePersonaLibrosElectronicos {
        [LibEnumDescription("Natural Domiciliado")]
        NaturalDomciliado = 0, 
        [LibEnumDescription("Jurídico Domiciliado")]
        JuridicoDomiciliado, 
        [LibEnumDescription("Natural No Domiciliado")]
        NaturalNoDomiciliado,
        [LibEnumDescription("Jurídico No Domiciliado")]
        JuridicoNoDomiciliado
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eCondicionDeImportacion {
        [LibEnumDescription("A Convenir", Index = 0)]
        [LibEnumDescription("A Convenir", Index = 1)]
        AConvenir = 0, 
        [LibEnumDescription("EXW - Ex Works", Index = 0)]
        [LibEnumDescription("EXW", Index = 1)]
        EXW, 
        [LibEnumDescription("FAS - Free Alongside Ship", Index = 0)]
        [LibEnumDescription("FAS", Index = 1)]
        FAS, 
        [LibEnumDescription("FOB - Free On Board", Index = 0)]
        [LibEnumDescription("FOB", Index = 1)]
        FOB, 
        [LibEnumDescription("FCA - Free Carrier", Index = 0)]
        [LibEnumDescription("FCA", Index = 1)]
        FCA, 
        [LibEnumDescription("CFR - Cost and Freight", Index = 0)]
        [LibEnumDescription("CFR", Index = 1)]
        CFR, 
        [LibEnumDescription("CIF - Cost, Insurance and Freight", Index = 0)]
        [LibEnumDescription("CIF", Index = 1)]
        CIF, 
        [LibEnumDescription("CPT - Carriage Paid To", Index = 0)]
        [LibEnumDescription("CPT", Index = 1)]
        CPT, 
        [LibEnumDescription("CIP - Carriage and Insurance Paid", Index = 0)]
        [LibEnumDescription("CIP", Index = 1)]
        CIP, 
        [LibEnumDescription("DAT - Delivered At Terminal", Index = 0)]
        [LibEnumDescription("DAT", Index = 1)]
        DAT, 
        [LibEnumDescription("DAP - Delivered At Place", Index = 0)]
        [LibEnumDescription("DAP", Index = 1)]
        DAP, 
        [LibEnumDescription("DDP - Delivered Duty Paid", Index = 0)]
        [LibEnumDescription("DDP", Index = 1)]
        DDP
	}
    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eCreditoFiscal {
        [LibEnumDescription("Deducible")]
        Deducible = 0,
        [LibEnumDescription("Prorrateable")]
        Prorrateable,
        [LibEnumDescription("No Deducible")]
        NoDeducible
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eReporteCostoDeCompras {
      [LibEnumDescription("Un Articulo")]
      UnArticulo = 0, 
      [LibEnumDescription("Una Linea")]
      UnaLineaDeProducto,
      [LibEnumDescription("Todos Los Articulos")]
      TodosLosArticulos
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eMonedaParaImpresion {
        [LibEnumDescription("En Bolívares")]
        EnSol = 0,
        [LibEnumDescription("En Moneda Original")]
        EnMonedaOriginal
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBuscarPor {
        [LibEnumDescription("Todas")]
        Todas = 0,
        [LibEnumDescription("Una")]
        Una
    }

} //End of namespace namespace Galac.Adm.Ccl.GestionCompras
