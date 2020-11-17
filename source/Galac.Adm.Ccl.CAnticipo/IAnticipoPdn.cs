using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Ccl.CAnticipo {

    public interface IAnticipoPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoAnticipo(int valConsecutivoCompania, int valConsecutivoAnticipo);
        bool GenerarAnticiposCobrados(string valNumeroDeCobranza, string valcodigocliente, string valSimboloMoneda, List<RenglonCobroDeFactura> ListDeCobro);
        #endregion //Metodos Generados


    } //End of class IAnticipoPdn

} //End of namespace Galac.Adm.Ccl.CAnticipo

