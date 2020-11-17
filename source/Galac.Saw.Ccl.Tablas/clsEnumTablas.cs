using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Tablas {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeFormaDePago {
        [LibEnumDescription("Efectivo")]
        Efectivo = 0, 
        [LibEnumDescription("Tarjeta")]
        Tarjeta, 
        [LibEnumDescription("Cheque")]
        Cheque, 
        [LibEnumDescription("Deposito")]
        Deposito,
        [LibEnumDescription("Anticipo")]
        Anticipo
	}


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusMaquinaFiscal {
        [LibEnumDescription("Activa")]
        Activa = 0, 
        [LibEnumDescription("Inactiva")]
        Inactiva
	}


} //End of namespace namespace Galac.Saw.Ccl.Tablas
