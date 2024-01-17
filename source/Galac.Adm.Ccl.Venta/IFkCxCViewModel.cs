using Galac.Saw.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkCxCViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string Numero { get; set; }
          eStatusCXC Status { get; set; }
          eTipoDeCxC TipoCxC { get; set; }
          string CodigoCliente { get; set; }
          string NombreCliente { get; set; }
          DateTime Fecha { get; set; }
          string NumeroDocumentoOrigen { get; set; }
          string CodigoTipoDeDocumentoLey { get; set; }
        #endregion //Propiedades

    } //End of class IFkCxCViewModel

} //End of namespace Galac..Ccl.ComponenteNoEspecificado

