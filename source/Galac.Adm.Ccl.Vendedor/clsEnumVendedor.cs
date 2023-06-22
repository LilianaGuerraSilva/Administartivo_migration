using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Vendedor {

	[TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusVendedor {
        [LibEnumDescription("Activo")]
        Activo = 0, 
        [LibEnumDescription("Inactivo")]
        Inactivo, 
        [LibEnumDescription("Restringido")]
        Restringido
	}

	[TypeConverter(typeof(LibEnumTypeConverter))]
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

    [TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eNivelDeComisionVentaYCobranza{
        [LibEnumDescription("Nivel 1")]
        Nivel1 = 0, 
        [LibEnumDescription("Nivel 2")]
        Nivel2, 
        [LibEnumDescription("Nivel 3")]
        Nivel3, 
        [LibEnumDescription("Nivel 4")]
        Nivel4, 
        [LibEnumDescription("Nivel 5")]
        Nivel5,
        [LibEnumDescription("Sin Asignar")]
        SinAsignar,
        [LibEnumDescription("< Nivel 1")]
        MenorANivel1
	}

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoComision {
        [LibEnumDescription("Por Porcentaje")]
        PorPorcentaje = 0,
        [LibEnumDescription("Por Monto")]
        PorMonto
    }
} //End of namespace namespace Galac.Adm.Ccl.Vendedor
