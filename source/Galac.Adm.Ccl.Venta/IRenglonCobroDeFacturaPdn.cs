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
        string BuscarCodigoFormaDelCobro(Saw.Ccl.Tablas.eTipoDeFormaDePago valTipoDeFormaDePago);
        #endregion //Metodos Generados
    }


 
 

} //End of class IFacturaRapidaPdn