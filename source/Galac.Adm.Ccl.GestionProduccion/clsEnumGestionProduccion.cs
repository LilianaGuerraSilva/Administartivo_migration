using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.GestionProduccion {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoStatusOrdenProduccion {
        [LibEnumDescription("Ingresada")]
        Ingresada = 0, 
        [LibEnumDescription("Iniciada")]
        Iniciada, 
        [LibEnumDescription("Cerrada")]
        Cerrada, 
        [LibEnumDescription("Anulada")]
        Anulada
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eGeneradoPor {
        [LibEnumDescription("Orden")]
        Orden = 0,
        [LibEnumDescription("Fecha")]
        Fecha
    }


} //End of namespace namespace Galac.Adm.Ccl.GestionProduccion
