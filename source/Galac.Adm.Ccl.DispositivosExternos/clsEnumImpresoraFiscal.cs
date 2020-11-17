using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.DispositivosExternos
{

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum ePuertoImpresoraFiscal {
        [LibEnumDescription("COM1")]
        COM1 = 0, 
        [LibEnumDescription("COM2")]
        COM2, 
        [LibEnumDescription("COM3")]
        COM3, 
        [LibEnumDescription("COM4")]
        COM4, 
        [LibEnumDescription("COM5")]
        COM5, 
        [LibEnumDescription("COM6")]
        COM6, 
        [LibEnumDescription("COM7")]
        COM7, 
        [LibEnumDescription("COM8")]
        COM8, 
        [LibEnumDescription("COM9")]
        COM9, 
        [LibEnumDescription("COM10")]
        COM10, 
        [LibEnumDescription("COM11")]
        COM11,
        [LibEnumDescription("COM12")]
        COM12, 
        [LibEnumDescription("COM13")]
        COM13, 
        [LibEnumDescription("COM14")]
        COM14, 
        [LibEnumDescription("COM15")]
        COM15, 
        [LibEnumDescription("COM16")]
        COM16                
	}  

} //End of namespace namespace Galac.Adm.Ccl.DispositivosExternos
