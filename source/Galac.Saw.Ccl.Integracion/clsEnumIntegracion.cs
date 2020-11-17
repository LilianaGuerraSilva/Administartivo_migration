using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Integracion {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoIntegracion {
        [LibEnumDescription("NOMINA")]
        NOMINA = 0, 
        [LibEnumDescription("RIS")]
        RIS
	}


} //End of namespace namespace Galac.Saw.Ccl.Integracion
