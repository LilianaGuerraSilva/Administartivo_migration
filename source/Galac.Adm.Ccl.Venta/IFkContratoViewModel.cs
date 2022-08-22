using System;

namespace Galac.Adm.Ccl.Venta
{
    public interface IFkContratoViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string NumeroContrato { get; set; }
          eStatusContrato StatusContrato { get; set; }
          string CodigoCliente { get; set; }
          string NombreCliente { get; set; }
          DateTime FechaDeInicio { get; set; }
        #endregion //Propiedades

    } //End of class IFkContratoViewModel

} //End of namespace Galac.Dbo.Ccl.Venta

