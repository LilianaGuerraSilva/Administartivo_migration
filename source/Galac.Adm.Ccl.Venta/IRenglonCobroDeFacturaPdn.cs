using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.Venta {
    public interface IRenglonCobroDeFacturaPdn : ILibPdn {
        #region Metodos Generados
        LibResponse  InsertRenglonCobroDeFactura(IList<RenglonCobroDeFactura> valRenglonCobroDeFactura);
        string BuscarCodigoFormaDelCobro(Adm.Ccl.Venta.eFormaDeCobro valTipoDeFormaDePago);
        string BuscarConsecutivoFormaDelCobro(eFormaDeCobro valTipoDeFormaDePago);
        string BuscarCodigoCtaBancariaFormaDelCobro(int consecutivoCompania, eFormaDeCobro valTipoDeFormaDePago);
        #endregion //Metodos Generados
    }


 
 

} //End of class IFacturaRapidaPdn