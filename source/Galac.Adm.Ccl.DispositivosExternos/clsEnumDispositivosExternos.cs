using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.DispositivosExternos {

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum ePuerto {
        [LibEnumDescription("COM1",Index =0)]
        [LibEnumDescription("1",Index = 1)]
        COM1 = 1,
        [LibEnumDescription("COM2",Index = 0)]
        [LibEnumDescription("2",Index = 1)]
        COM2,         
        [LibEnumDescription("COM3",Index = 0)]
        [LibEnumDescription("3",Index = 1)]
        COM3,
        [LibEnumDescription("COM4",Index = 0)]
        [LibEnumDescription("4",Index = 1)]
        COM4,
        [LibEnumDescription("COM5",Index = 0)]
        [LibEnumDescription("5",Index = 1)]
        COM5,
        [LibEnumDescription("COM6",Index = 0)]
        [LibEnumDescription("6",Index = 1)]
        COM6,
        [LibEnumDescription("COM7",Index = 0)]
        [LibEnumDescription("7",Index = 1)]
        COM7,
        [LibEnumDescription("COM8",Index = 0)]
        [LibEnumDescription("8",Index = 1)]
        COM8,
        [LibEnumDescription("COM9",Index = 0)]
        [LibEnumDescription("9",Index = 1)]
        COM9,
        [LibEnumDescription("COM10",Index = 0)]
        [LibEnumDescription("10",Index = 1)]
        COM10,
        [LibEnumDescription("COM11",Index = 0)]
        [LibEnumDescription("11",Index = 1)]        
        COM11,
        [LibEnumDescription("COM12",Index = 0)]
        [LibEnumDescription("12",Index = 1)]
        COM12,
        [LibEnumDescription("COM13",Index = 0)]
        [LibEnumDescription("13",Index = 1)]
        COM13,
        [LibEnumDescription("COM14",Index = 0)]
        [LibEnumDescription("14",Index = 1)]
        COM14,
        [LibEnumDescription("COM15",Index = 0)]
        [LibEnumDescription("15",Index = 1)]
        COM15,
        [LibEnumDescription("COM16",Index = 0)]
        [LibEnumDescription("16",Index = 1)]
        COM16
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBitsDeDatos {
        [LibEnumDescription("6")]
        d6 = 0, 
        [LibEnumDescription("7")]
        d7, 
        [LibEnumDescription("8")]
        d8
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eParidad {
        [LibEnumDescription("Ninguna")]
        Ninguna = 0, 
        [LibEnumDescription("Par")]
        Par, 
        [LibEnumDescription("Impar")]
        Impar, 
        [LibEnumDescription("Marca")]
        Marca, 
        [LibEnumDescription("Espacio")]
        Espacio
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eBitsDeParada {
        [LibEnumDescription("Ninguno")]
        Ninguno = 0, 
        [LibEnumDescription("Uno")]
        Uno, 
        [LibEnumDescription("Uno Y Medio")]
        UnoYMedio, 
        [LibEnumDescription("Dos")]
        Dos
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eBaudRate {
        [LibEnumDescription("600")]
        b600 = 0, 
        [LibEnumDescription("1200")]
        b1200, 
        [LibEnumDescription("2400")]
        b2400, 
        [LibEnumDescription("4800")]
        b4800, 
        [LibEnumDescription("9600")]
        b9600, 
        [LibEnumDescription("19200")]
        b19200, 
        [LibEnumDescription("28800")]
        b28800, 
        [LibEnumDescription("38400")]
        b38400, 
        [LibEnumDescription("56000")]
        b56000, 
        [LibEnumDescription("128000")]
        b128000
	}

	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eControlDeFlujo {
        [LibEnumDescription("Ninguno")]
        Ninguno = 0, 
        [LibEnumDescription("Xon Off")]
        XonOff, 
        [LibEnumDescription("Request To Send")]
        RequestToSend, 
        [LibEnumDescription("Request To Send XOnXOff")]
        RequestToSendXOnXOff
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoConexion {
        [LibEnumDescription("Puerto Serial")]
        PuertoSerial = 0,
        [LibEnumDescription("USB")]
        USB,
        [LibEnumDescription("LAN")]
        LAN
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eModeloDeBalanza {
        [LibEnumDescription("Xacta")]
        Xacta = 0,
        [LibEnumDescription("Aclas OS 2X")]
        AclasOS2X,
        [LibEnumDescription("OHAUS T-23 / T-33")]
        OHAUST23,
        [LibEnumDescription("CAS PR II")]
        CAS_PR_II,
        [LibEnumDescription("CAS JP 2")]
        CAS_JP_2
    }    

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusImpresorasFiscales {
        [LibEnumDescription("Listo En Espera")]
        eListoEnEspera = 0,
        [LibEnumDescription("Poco Papel")]
        ePocoPapel,
        [LibEnumDescription("Fin del Papel/Atasco del Papel")]
        eAtascoDePapel,
        [LibEnumDescription("Fin del Papel")]
        eSinPapel
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFamiliaImpresoraFiscal {
        [LibEnumDescription("THEFACTORY")]
        THEFACTORY=0,
        [LibEnumDescription("ELEPOS VMAX")]
        ELEPOSVMAX,
        [LibEnumDescription("BEMATECH")]
        BEMATECH,
        [LibEnumDescription("EPSON PNP")]
        EPSONPNP,
        [LibEnumDescription("QPRINT")]
        QPRINT,
        [LibEnumDescription("BMC")]
        BMC,
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eImpresoraFiscal {
        [LibEnumDescription("Epson PF 220",Index=0)]
        [LibEnumDescription("EPSON PNP",Index = 1)]
        EPSON_PF_220 = 0,
        [LibEnumDescription("Epson PF 300",Index =0)]
        [LibEnumDescription("EPSON PNP",Index = 1)]
        EPSON_PF_300,
        [LibEnumDescription("Epson TM 675 PF",Index =0)]
        [LibEnumDescription("EPSON PNP",Index = 1)]
        EPSON_TM_675_PF,
        [LibEnumDescription("EPSON TM 950 PF",Index =0)]
        [LibEnumDescription("EPSON PNP",Index = 1)]
        EPSON_TM_950_PF,
        [LibEnumDescription("Bematech MP-20 FI-II",Index =0)]
        [LibEnumDescription("BEMATECH",Index = 1)]
        BEMATECH_MP_20_FI_II,
        [LibEnumDescription("Bixolon SRP 270",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        BIXOLON270,
        [LibEnumDescription("Epson PF 220-II",Index =0)]
        [LibEnumDescription("EPSON PNP",Index = 1)]
        EPSON_PF_220II,
        [LibEnumDescription("OKI ML 1120",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        OKIML1120,
        [LibEnumDescription("Bixolon SRP 350",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        BIXOLON350,
        [LibEnumDescription("Aclas PP1F3",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        ACLASPP1F3,
        [LibEnumDescription("BMC CAMEL",Index =0)]
        [LibEnumDescription("BMC",Index = 1)]
        BMC_CAMEL,
        [LibEnumDescription("BMC SPARK 614",Index =0)]
        [LibEnumDescription("BMC",Index = 1)]
        BMC_SPARK_614,
        [LibEnumDescription("Bematech MP 2100 FI",Index =0)]
        [LibEnumDescription("BEMATECH",Index = 1)]
        BEMATECH_MP_2100_FI,
        [LibEnumDescription("DASCOM TALLY 1125",Index=0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        DASCOMTALLY1125,
        [LibEnumDescription("QPRINT MF",Index =0)]
        [LibEnumDescription("QPRINT",Index = 1)]
        QPRINT_MF,
        [LibEnumDescription("ELEPOS VMAX 580",Index =0)]
        [LibEnumDescription("ELEPOS VMAX",Index = 1)]
        ELEPOSVMAX_580,
        [LibEnumDescription("DASCOM TALLY DT230",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        DASCOMTALLYDT230,
        [LibEnumDescription("Bixolon SRP 280",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        BIXOLON280,
        [LibEnumDescription("ACLAS PP9",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        ACLASPP9,
        [LibEnumDescription("Bixolon SRP 812",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        BIXOLON812,
        [LibEnumDescription("HKA80",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        HKA80,
        [LibEnumDescription("HKA112",Index =0)]
        [LibEnumDescription("THEFACTORY",Index = 1)]
        HKA112,
        [LibEnumDescription("BMC TH34 EJ",Index = 0)]
        [LibEnumDescription("BMC",Index = 1)]
        BMC_TH34_EJ,
        [LibEnumDescription("Bematech MP 4000 FI",Index = 0)]
        [LibEnumDescription("BEMATECH",Index = 1)]
        BEMATECH_MP_4000_FI,
        [LibEnumDescription("ELEPOS VMAX 220 F",Index = 0)]
        [LibEnumDescription("ELEPOS VMAX",Index = 1)]
        ELEPOSVMAX_220_F,
        [LibEnumDescription("ELEPOS VMAX 300",Index = 0)]
        [LibEnumDescription("ELEPOS VMAX",Index = 1)]
        ELEPOSVMAX_300,
        [LibEnumDescription("DASCOM TALLY 1140", Index = 0)]
        [LibEnumDescription("THEFACTORY", Index = 1)]
        DASCOMTALLY1140,
        [LibEnumDescription("ACLAS PP9 PLUS", Index = 0)]
        [LibEnumDescription("THEFACTORY", Index = 1)]
        ACLASPP9_PLUS
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eFormaDeCobroImprFiscal {
        [LibEnumDescription("Efectivo", Index = 0)]
        [LibEnumDescription("00001", Index = 1)]
        Efectivo = 0,
        [LibEnumDescription("Tarjeta", Index = 0)]
        [LibEnumDescription("00002", Index = 1)]
        Tarjeta,
        [LibEnumDescription("Cheque", Index = 0)]
        [LibEnumDescription("00003", Index = 1)]
        Cheque,
        [LibEnumDescription("Depósito", Index = 0)]
        [LibEnumDescription("00004", Index = 1)]
        Deposito,
        [LibEnumDescription("Anticipo", Index = 0)]
        [LibEnumDescription("00005", Index = 1)]
        Anticipo,
        [LibEnumDescription("Transferencia", Index = 0)]
        [LibEnumDescription("00006", Index = 1)]
        Transferencia,
        [LibEnumDescription("Vuelto Efectivo", Index = 0)]
        [LibEnumDescription("00007", Index = 1)]
        VueltoEfectivo,
        [LibEnumDescription("Vuelto Pago Móvil", Index = 0)]
        [LibEnumDescription("00008", Index = 1)]
        VueltoC2P,
        [LibEnumDescription("Tarjeta Medios Electrónicos")]
        [LibEnumDescription("00009", Index = 1)]
        TarjetaMS,
        [LibEnumDescription("Zelle")]
        [LibEnumDescription("00010", Index = 1)]
        Zelle,
        [LibEnumDescription("P2C")]
        [LibEnumDescription("00011", Index = 1)]
        PagoMovil,
        [LibEnumDescription("Transferencia Medios Electrónicos")]
        [LibEnumDescription("00012", Index = 1)]
        TransferenciaMS,
        [LibEnumDescription("C2P")]
        [LibEnumDescription("00013", Index = 1)]
        C2P
    }

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipoDocumentoFiscal {
        [LibEnumDescription("Factura Fiscal")]
        FacturaFiscal = 0,
        [LibEnumDescription("Nota de Crédito")]
        NotadeCredito,
        [LibEnumDescription("Reporte Z")]
        ReporteZ,
        [LibEnumDescription("Reporte X")]
        ReporteX,
    }     
} //End of namespace namespace Galac.Saw.Ccl.DispositivosExternos
