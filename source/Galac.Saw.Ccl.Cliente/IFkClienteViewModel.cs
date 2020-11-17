using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Cliente {

    public interface IFkClienteViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        int Consecutivo { get; set; }
        string Codigo { get; set; }
        string Nombre { get; set; }
        string NumeroRIF { get; set; }
        string StatusStr { get; set; }
        string Direccion { get; set; }
        DateTime ClienteDesdeFecha { get; set; }
        string TipoDeContribuyente { get; set; }
        #endregion //Propiedades


    } //End of class IFkClienteViewModel

} //End of namespace Galac.Saw.Ccl.Cliente

