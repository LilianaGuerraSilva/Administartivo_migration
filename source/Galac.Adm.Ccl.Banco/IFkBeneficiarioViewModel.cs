using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.Banco;
namespace Galac.Adm.Ccl.Banco {

    public interface IFkBeneficiarioViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Codigo { get; set; }
          string NumeroRIF { get; set; }
          string NombreBeneficiario { get; set; }
          eTipoDeBeneficiario TipoDeBeneficiario { get; set; }
        #endregion //Propiedades


    } //End of class IFkBeneficiarioViewModel

} //End of namespace Galac.Adm.Ccl.Banco

