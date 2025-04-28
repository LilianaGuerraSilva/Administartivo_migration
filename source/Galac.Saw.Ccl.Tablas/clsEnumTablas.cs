using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Tablas {

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusMaquinaFiscal {
        [LibEnumDescription("Activa")] Activa = 0,
        [LibEnumDescription("Inactiva")] Inactiva
	}
    
    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusOtrosCargosyDescuentosDeFactura {
        [LibEnumDescription("Vigente")] Vigente = 0,
        [LibEnumDescription("Desincorporado")] Desincorporado
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBaseCalculoOtrosCargosDeFactura {
        [LibEnumDescription("Fórmula")] Formula = 0,
        [LibEnumDescription("Monto Fijo")] MontoFijo,
        [LibEnumDescription("Indicar Monto")] IndicarMonto
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBaseFormulaOtrosCargosDeFactura {
        [LibEnumDescription("Sub-Total")] SubTotal = 0,
        [LibEnumDescription("Base Imponible")] BaseImponible,
        [LibEnumDescription("Total Factura")] TotalFactura
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComoAplicaOtrosCargosDeFactura {
        [LibEnumDescription("Suma")] Suma = 0,
        [LibEnumDescription("Resta")] Resta,
        [LibEnumDescription("No Aplica (solo informativo)")] NoAplica
    }

} //End of namespace namespace Galac.Saw.Ccl.Tablas