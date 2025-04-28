
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.ImprentaDigital;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaDigitalCreator {
        public static clsImprentaDigitalBase Create(eProveedorImprentaDigital valProveedorImprenta, eTipoDocumentoFactura valTipoDocumentoFactura, string valNumeroFactura, eTipoDocumentoImprentaDigital valTipoDocumentoImprentaDigital) {
            switch (valProveedorImprenta) {                
                case eProveedorImprentaDigital.TheFactoryHKA:
                    return new ImprentaTheFactory(valTipoDocumentoFactura, valNumeroFactura, valTipoDocumentoImprentaDigital);
                case eProveedorImprentaDigital.Novus:
                    return new ImprentaNovus(valTipoDocumentoFactura, valNumeroFactura, valTipoDocumentoImprentaDigital);
                case eProveedorImprentaDigital.Unidigital:
                    return new ImprentaUnidigital(valTipoDocumentoFactura, valNumeroFactura, valTipoDocumentoImprentaDigital);
                default:
                    throw new GalacException("Proveedor de Imprenta Digital aún no implementado.", eExceptionManagementType.Controlled);
            }
        }
    }
}
