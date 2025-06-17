using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Cliente.Ccl {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eStatusCliente {
		[LibEnumDescription("Activo")]
		Activo = 0, 
		[LibEnumDescription("Inactivo")]
		Inactivo, 
		[LibEnumDescription("Restringido")]
		Restringido, 
		[LibEnumDescription("Suspendido")]
		Suspendido
	}


} //End of namespace namespace Galac.Cliente.Ccl
