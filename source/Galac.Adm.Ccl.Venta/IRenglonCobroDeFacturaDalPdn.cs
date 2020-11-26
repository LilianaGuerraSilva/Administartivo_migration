using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Ccl.Venta {
    public interface IRenglonCobroDeFacturaDalPdn {
        LibResponse InsertChild(int valConsecutivoCompania, string valNumeroFactura,eTipoDocumentoFactura valTipoDocumento, List<RenglonCobroDeFactura> valRecord);
        //LibResponse InsertChild(  RenglonCobroDeFactura valRecord  );
    }
}
