using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.DispositivosExternos
{
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eBitsDeDatos {
        [LibEnumDescription("8")]
        b8 = 0,
        [LibEnumDescription("7")]
        b7, 
        [LibEnumDescription("6")]
        b6, 
        [LibEnumDescription("5")]
        b5
	}
	

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eModeloDeBalanza {
        [LibEnumDescription("XACTA")]
        XACTA = 0, 
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eParidad {
        [LibEnumDescription("None")]
        None = 0,
        [LibEnumDescription("Odd")]
        Odd, 
        [LibEnumDescription("Even")]
        Even, 
        [LibEnumDescription("Mark")]
        Mark,        
        [LibEnumDescription("Space")]
        Space
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eBitsDeParada {
        [LibEnumDescription("None")]
        None = 0,         
        [LibEnumDescription("One")]        
        One, 
        [LibEnumDescription("Two")]
        Two,
        [LibEnumDescription("OnePointFive")]
        OnePointFive        
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eControlDeFlujo {
        [LibEnumDescription("None")]
        None = 0, 
         [LibEnumDescription("XOnXOff")]
        XOnXOff,
        [LibEnumDescription("RequestToSend")]
        RequestToSend, 
        [LibEnumDescription("RequestToSendXOnXoff")]
        RequestToSendXOnXoff       
	}

	

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eBaudRate {
        [LibEnumDescription("75")]
        v75=0, 
        [LibEnumDescription("110")]
        v110, 
        [LibEnumDescription("134")]
        v134, 
        [LibEnumDescription("150")]
        v150, 
        [LibEnumDescription("300")]
        v300, 
        [LibEnumDescription("600")]
        v600, 
        [LibEnumDescription("1200")]
        v1200, 
        [LibEnumDescription("1800")]
        v1800, 
        [LibEnumDescription("2400")]
        v2400, 
        [LibEnumDescription("4800")]
        v4800, 
        [LibEnumDescription("7200")]
        v7200, 
        [LibEnumDescription("9600")]
        v9600, 
        [LibEnumDescription("14400")]
        v14400, 
        [LibEnumDescription("19200")]
        v19200, 
        [LibEnumDescription("38400")]
        v38400, 
        [LibEnumDescription("57600")]
        v57600, 
        [LibEnumDescription("115200")]
        v115200, 
        [LibEnumDescription("128000")]
        v128000
	}
	
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum ePuertoDeCaja {
        [LibEnumDescription("COM1")]
        COM1 = 1, 
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

} //End of namespace namespace Galac.Adm.Ccl.Balanza
