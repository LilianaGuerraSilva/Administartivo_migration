using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Cliente {
	[System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDeContribuyente {
        [LibEnumDescription("Contribuyente")]
        Contribuyente = 0,
        [LibEnumDescription("No Contribuyente")]
        NoContribuyente
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eStatusCliente {
        [LibEnumDescription("Activo")]
        Activo = 0, 
        [LibEnumDescription("Inactivo")]
        Inactivo, 
        [LibEnumDescription("Restringido")]
        Restringido, 
        [LibEnumDescription("Suspendido")]
        Suspendido,
        [LibEnumDescription("Tiempo Sin Contacto")]
        Tiempo_Sin_Contacto,
        [LibEnumDescription("Datos Desactualizados")]
        Datos_Desactualizados
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eOrigenFacturacionOManual {
        [LibEnumDescription("Factura")]
        Factura = 0, 
        [LibEnumDescription("Manual")]
        Manual
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eNivelDePrecio {
        [LibEnumDescription("Precio 1")]
        Precio1 = 0, 
        [LibEnumDescription("Precio 2")]
        Precio2, 
        [LibEnumDescription("Precio 3")]
        Precio3, 
        [LibEnumDescription("Precio 4")]
        Precio4
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eInfoGalacModoEnvio {
        [LibEnumDescription("Por  Definir")]
        PorDefinir = 0, 
        [LibEnumDescription("fax")]
        fax, 
        [LibEnumDescription("e -mail")]
        email, 
        [LibEnumDescription("Mail y  Fax")]
        MailyFax, 
        [LibEnumDescription("No por  Ahora")]
        NoporAhora, 
        [LibEnumDescription("No tiene fax -mail")]
        Notienefaxmail, 
        [LibEnumDescription("Ya  Suscrito")]
        YaSuscrito
	}

    [System.ComponentModel.TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoDocumentoIdentificacion {
        [LibEnumDescription("RUC",Index =0)]
        [LibEnumDescription("RIF",Index = 1)]
        RUC = 0, 
        [LibEnumDescription("DNI",Index = 0)]
        [LibEnumDescription("CEDULA",Index = 1)]
        DNI, 
        [LibEnumDescription("Carnet de  Extranjería")]
        CarnetdeExtranjeria, 
        [LibEnumDescription("Pasaporte")]
        Pasaporte, 
        [LibEnumDescription("Otros  Tipos de  Documentos")]
        OtrosTiposdeDocumentos
	}


} //End of namespace namespace Galac.Saw.Ccl.Cliente
