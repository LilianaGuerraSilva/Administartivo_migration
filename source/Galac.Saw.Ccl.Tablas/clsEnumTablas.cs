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
        [LibEnumDescription("Depósito")]
        Deposito,
        [LibEnumDescription("Anticipo")]
        Anticipo,
        [LibEnumDescription("Vuelto Efectivo")]
        VueltoEfectivo,
		[LibEnumDescription("VueltoC2P")]
        VueltoC2P
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusMaquinaFiscal {
        [LibEnumDescription("Activa")]
        Activa = 0, 
        [LibEnumDescription("Inactiva")]
        Inactiva
	}


} //End of namespace namespace Galac.Saw.Ccl.Tablas
