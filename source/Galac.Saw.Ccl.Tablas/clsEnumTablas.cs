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
        [LibEnumDescription("Transferencia")]
        Transferencia,
        [LibEnumDescription("Vuelto Efectivo")]
        VueltoEfectivo,
        [LibEnumDescription("Vuelto C2P")]
        VueltoC2P,
        [LibEnumDescription("Tarjeta Medios Electrónicos")]
        TarjetaMS,
        [LibEnumDescription("Zelle")]
        Zelle,
        [LibEnumDescription("P2C")]
        PagoMovil,
        [LibEnumDescription("Transferencia Medios Electrónicos")]
        TransferenciaMS,
        [LibEnumDescription("C2P")]
        C2P
    }


    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusMaquinaFiscal {
        [LibEnumDescription("Activa")]
        Activa = 0, 
        [LibEnumDescription("Inactiva")]
        Inactiva
	}


} //End of namespace namespace Galac.Saw.Ccl.Tablas
