using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Vehiculo {

    public interface IFkVehiculoViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Placa { get; set; }
          string serialVIN { get; set; }
          string NombreModelo { get; set; }
          //string Marca { get; set; }
          //int Ano { get; set; }
          string CodigoColor { get; set; }
          //string DescripcionColor { get; set; }
          string CodigoCliente { get; set; }
          string NombreCliente { get; set; }
        #endregion //Propiedades


    } //End of class IFkVehiculoViewModel

} //End of namespace Galac.Saw.Ccl.Vehiculo

