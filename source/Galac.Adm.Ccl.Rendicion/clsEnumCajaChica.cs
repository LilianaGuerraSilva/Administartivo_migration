using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.CajaChica {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusDocumento {
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
	public enum eTipoDeCxC {
        [LibEnumDescription("Factura")]
        Factura = 0, 
        [LibEnumDescription("Cheque Devuelto")]
        ChequeDevuelto, 
        [LibEnumDescription("Nota De Credito")]
        NotaDeCredito, 
        [LibEnumDescription("Nota De Debito")]
        NotaDeDebito, 
        [LibEnumDescription("Nota Entrega")]
        NotaEntrega, 
        [LibEnumDescription("Giro")]
        Giro, 
        [LibEnumDescription("No Asignado")]
        NoAsignado
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipodePersonaRetencion {
        [LibEnumDescription("P J_ Domiciliada")]
        PJ_Domiciliada = 0, 
        [LibEnumDescription("P J_ No Domiciliada")]
        PJ_NoDomiciliada, 
        [LibEnumDescription("P N_ Residente")]
        PN_Residente, 
        [LibEnumDescription("P N_ No Residente")]
        PN_NoResidente
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeCompra {
        [LibEnumDescription("Compras Exentas")]
        ComprasExentas = 0, 
        [LibEnumDescription("Compras Importacion")]
        ComprasImportacion, 
        [LibEnumDescription("Compras Nacionales")]
        ComprasNacionales, 
        [LibEnumDescription("Compras Exoneradas")]
        ComprasExoneradas, 
        [LibEnumDescription("Compras No Sujetas")]
        ComprasNoSujetas, 
        [LibEnumDescription("Compras Sin Derecho Acredito")]
        ComprasSinDerechoAcredito
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
	public enum eTipoDeProveedorDeLibrosFiscales {
        [LibEnumDescription("Con  Rif")]
        ConRif = 0, 
        [LibEnumDescription("Sin  Rif")]
        SinRif, 
        [LibEnumDescription("No  Residenciado")]
        NoResidenciado, 
        [LibEnumDescription("No  Domiciliado")]
        NoDomiciliado
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eDondeSeEfectuaLaRetencionIVA {
        [LibEnumDescription("No  Retenida")]
        NoRetenida = 0, 
        [LibEnumDescription("Cx P")]
        CxP, 
        [LibEnumDescription("Pago")]
        Pago
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeContribuyenteDelIva {
        [LibEnumDescription("Contribuyente  Formal")]
        ContribuyenteFormal = 0, 
        [LibEnumDescription("Contribuyente  Ordinario")]
        ContribuyenteOrdinario, 
        [LibEnumDescription("Contribuyente  Especial")]
        ContribuyenteEspecial, 
        [LibEnumDescription("Conversion")]
        Conversion
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusAnticipo {
        [LibEnumDescription("Vigente")]
        Vigente = 0, 
        [LibEnumDescription("Anulado")]
        Anulado, 
        [LibEnumDescription("Parcialmente  Usado")]
        ParcialmenteUsado, 
        [LibEnumDescription("Completamente  Usado")]
        CompletamenteUsado, 
        [LibEnumDescription("Completamente  Devuelto")]
        CompletamenteDevuelto, 
        [LibEnumDescription("Parcialmente  Devuelto")]
        ParcialmenteDevuelto
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeAnticipo {
        [LibEnumDescription("Cobrado")]
        Cobrado = 0, 
        [LibEnumDescription("Pagado")]
        Pagado,
        [LibEnumDescription("Adelanto")]
        Adelanto
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDocumentoIdentificacion {
        [LibEnumDescription("R UC")]
        RUC = 0, 
        [LibEnumDescription("D NI")]
        DNI, 
        [LibEnumDescription("Carnet de  Extranjería")]
        CarnetdeExtranjeria, 
        [LibEnumDescription("Pasaporte")]
        Pasaporte, 
        [LibEnumDescription("Otros  Tipos de  Documentos")]
        OtrosTiposdeDocumentos
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusRendicion {
        [LibEnumDescription("En Proceso")]
        EnProceso = 0, 
        [LibEnumDescription("Cerrada")]
        Cerrada, 
        [LibEnumDescription("Anulada")]
        Anulada
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusDetraccion {
        [LibEnumDescription("Autodetraccion")]
        Autodetraccion = 0, 
        [LibEnumDescription("Detraccion Por Tercero")]
        DetraccionPorTercero, 
        [LibEnumDescription("No Aplicada")]
        NoAplicada
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeTransaccionMunicipal {
        [LibEnumDescription("No Aplica")]
        NoAplica = 0, 
        [LibEnumDescription("Compra")]
        Compra, 
        [LibEnumDescription("Servicio")]
        Servicio
	}
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeDocumentoRendicion {
        [LibEnumDescription("Redicion")]
        Redicion = 0, 
        [LibEnumDescription("Reposicion")]
        Reposicion
	}

} //End of namespace namespace Galac.Saw.Ccl.Rendicion
