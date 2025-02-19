using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Tablas {

    public interface IFkOtrosCargosDeFacturaViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        string Codigo { get; set; }
        string Descripcion { get; set; }
        eStatusOtrosCargosyDescuentosDeFactura Status { get; set; }
        eBaseCalculoOtrosCargosDeFactura SeCalculaEnBasea { get; set; }
        eComoAplicaOtrosCargosDeFactura ComoAplicaAlTotalFactura { get; set; }
        decimal Monto { get; set; }
        decimal PorcentajeSobreBase {  get; set; }
        decimal Sustraendo {  get; set; }
        decimal PorcentajeComision {  get; set; }
        bool ExcluirDeComision { set; get; }
        eBaseFormulaOtrosCargosDeFactura BaseFormula {  get; set; }
        #endregion //Propiedades
    } //End of class IFkOtrosCargosDeFacturaViewModel

} //End of namespace Galac.Saw.Ccl.Tablas

