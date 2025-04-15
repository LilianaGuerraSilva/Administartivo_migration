
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaDigitalCreator {
        public static clsImprentaDigitalBase Create(eProveedorImprentaDigital valProveedorImprenta, eTipoDocumentoFactura valTipoDocumentoFactura, string valNumeroFactura, eTipoComprobantedeRetencion valTipoComprobantedeRetencion) {
            switch (valProveedorImprenta) {                
                case eProveedorImprentaDigital.TheFactoryHKA:
                    return new ImprentaTheFactory(valTipoDocumentoFactura, valNumeroFactura, valTipoComprobantedeRetencion);
                case eProveedorImprentaDigital.Novus:
                    return new ImprentaNovus(valTipoDocumentoFactura, valNumeroFactura, valTipoComprobantedeRetencion);
                case eProveedorImprentaDigital.Unidigital:
                    return new ImprentaUnidigital(valTipoDocumentoFactura, valNumeroFactura, valTipoComprobantedeRetencion);
                default:
                    throw new GalacException("Proveedor de Imprenta Digital aún no implementado.", eExceptionManagementType.Controlled);
            }
        }
    }
}
