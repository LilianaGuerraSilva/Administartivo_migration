
using LibGalac.Aos.Catching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl;
using Galac.Adm.Ccl.ImprentaDigital;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class ImprentaDigitalCreator {
        public static clsImprentaDigitalBase Create(eProveedorImprentaDigital valProveedorImprenta, eTipoDocumentoFactura valTipoDocumentoFactura, string valNumeroFactura) {
            switch (valProveedorImprenta) {                
                case eProveedorImprentaDigital.TheFactoryHKA:
                    return new ImprentaTheFactory(valTipoDocumentoFactura, valNumeroFactura);
                default:
                    throw new GalacException("Proveedor de Imprenta aún no implementado", eExceptionManagementType.Controlled);
            }
        }
    }
}
