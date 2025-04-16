using System;
using System.ComponentModel;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.ImprentaDigital {
      

    [TypeConverter(typeof(LibEnumTypeConverter))]
    public enum eTipodeTransaccionImprentaDigital {
        [LibEnumDescription("Facturaci�n")]
        Facturacion = 0,
        [LibEnumDescription("Comprobantes de Retenci�n")]
        ComprobantesDeRetencion
    }
    #region Comandos Thefactory HKA

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

    #endregion Comandos Thefactory HKA

    #region Comandos Novus
    public enum eComandosPostNovus {
        [LibEnumDescription("/Autenticacion/v3")]
        Autenticacion = 0,
        [LibEnumDescription("/facturacion/v3")]
        Emision,
        [LibEnumDescription("/Anulacion/v3")]
        Anular,
        [LibEnumDescription("/email/v3")]
        Email,
        [LibEnumDescription("/facturacion/status")]
        EstadoDocumento
    }
    #endregion Comandos Novus

    #region Comandos Unidigital
    public enum eComandosPostUnidigital {
        [LibEnumDescription("/user/login")]
        Autenticacion = 0,
        [LibEnumDescription("/documents/createandapprove")]
        Emision,
        [LibEnumDescription("/documents/anulled")]
        Anular,
        [LibEnumDescription("/documents/notified")]
        Email,
        [LibEnumDescription("/documents/searchbynumberandserie")]
        EstadoDocumento,
        [LibEnumDescription("/documents?strongId=")]
        ObtenerNroControl
    }
    #endregion Comandos Unidigital
} //End of namespace namespace Galac.Saw.Ccl.ImprentaDigital
