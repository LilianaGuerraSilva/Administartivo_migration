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
        [LibEnumDescription("Dep�sito")]
        Deposito,
        [LibEnumDescription("Anticipo")]
        Anticipo,
        [LibEnumDescription("Vuelto Efectivo")]
        VueltoEfectivo,
		[LibEnumDescription("Vuelto C2P")]
        VueltoC2P, 
        [LibEnumDescription("Cobro con Tarjeta")]
        CobroConTarjeta,
        [LibEnumDescription("Cobro Zelle")]
        CobroZelle,
        [LibEnumDescription("Cobro P2C")]
        CobroPagoMovil,
        [LibEnumDescription("Cobro con Transferencia")]
        CobroTransferencia,
        [LibEnumDescription("Cobro C2P")]
        CobroC2P
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusMaquinaFiscal {
        [LibEnumDescription("Activa")]
        Activa = 0, 
        [LibEnumDescription("Inactiva")]
        Inactiva
	}


} //End of namespace namespace Galac.Saw.Ccl.Tablas
