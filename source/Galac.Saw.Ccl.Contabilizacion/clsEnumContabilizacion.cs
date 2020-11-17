using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Contabilizacion {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeContabilizacion {
        [LibEnumDescription("Cada Documento")]
        CadaDocumento = 0, 
        [LibEnumDescription("Por Lote")]
        PorLote
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eDondeEfectuoContabilizacionRetIVA {
        [LibEnumDescription("No Contabilizada")]
        NoContabilizada = 0, 
        [LibEnumDescription("CxP")]
        CxP, 
        [LibEnumDescription("Pago")]
        Pago
	}


	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eContabilizacionIndividual {
        [LibEnumDescription("Inmediata")]
        Inmediata = 0, 
        [LibEnumDescription("Pospuesta")]
        Pospuesta
	}


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eContabilizacionPorLote {
        [LibEnumDescription("Diaria")]
        Diaria = 0, 
        [LibEnumDescription("Mensual")]
        Mensual
	}


} //End of namespace namespace Galac.Saw.Ccl.Contabilizacion
