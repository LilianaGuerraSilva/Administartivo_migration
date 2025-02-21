using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.ImprentaDigital {

	[TypeConverter(typeof(LibEnumTypeConverter))]
	public enum eTipoProveedorImprentaDigital {
        [LibEnumDescription("Normal")]
        Normal = 0, 
        [LibEnumDescription("Sin Rif")]
        SinRif, 
        [LibEnumDescription("No Domiciliado")]
        NoDomiciliado, 
        [LibEnumDescription("No Residenciado")]
        NoResidenciado
	} 

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eComandosPostTheFactoryHKA {
        [LibEnumDescription("/api/Autenticacion")]
        Autenticacion = 0,
        [LibEnumDescription("/api/Emision")]
        Emision,
        [LibEnumDescription("/api/Anular")]
        Anular,
        [LibEnumDescription("/api/EstadoDocumento")]
        EstadoDocumento,
        [LibEnumDescription("/api/EstadoLote")]
        EstadoLote,
        [LibEnumDescription("/api/ListadoDocumentos")]
        ListadoDocumentos,
        [LibEnumDescription("/api/DescargaArchivo")]
        DescargaArchivo,
        [LibEnumDescription("/api/AplicarRetencion")]
        AplicarRetencion,
        [LibEnumDescription("/api/EnviarCorreo")]
        EnviarCorreo,
        [LibEnumDescription("/api/RastrearCorreo")]
        RastrearCorreo
    }



} //End of namespace namespace Galac.Saw.Ccl.ImprentaDigital
