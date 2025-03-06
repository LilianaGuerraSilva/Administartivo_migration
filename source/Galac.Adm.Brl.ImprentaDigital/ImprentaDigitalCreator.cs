
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaDigitalCreator {
        public static clsImprentaDigitalBase Create(eProveedorImprentaDigital valProveedorImprenta, eTipoDocumentoFactura valTipoDocumentoFactura, string valNumeroFactura) {
            switch (valProveedorImprenta) {                
                case eProveedorImprentaDigital.TheFactoryHKA:
                    return new ImprentaTheFactory(valTipoDocumentoFactura, valNumeroFactura);
                case eProveedorImprentaDigital.Novus:
                    return new ImprentaNovus(valTipoDocumentoFactura, valNumeroFactura);
                case eProveedorImprentaDigital.Unidigital:
                    return new ImprentaUnidigital(valTipoDocumentoFactura, valNumeroFactura);
                default:
                    throw new GalacException("Proveedor de Imprenta Digital aún no implementado.", eExceptionManagementType.Controlled);
            }
        }
    }
}
