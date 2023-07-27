using Galac.Saw.Brl.Inventario;
using Galac.Saw.Lib.Uil;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CuadroDeBusquedaDeUbicacionDeArticulosViewModel : SearchBoxViewModelBase {
        const string CuadroDeBusquedaDeUbicacionDeArticulosViewModelPropertyName = "CuadroDeBusquedaDeUbicacionDeArticulosViewModel";

        public CuadroDeBusquedaDeUbicacionDeArticulosViewModel() : base(CuadroDeBusquedaDeUbicacionDeArticulosViewModelPropertyName, eTypeOfSearchInDb.Codigo_Descripcion) {
            DataService = new clsArticuloInventarioNav();
            IsControlVisible = false;
        }
    }
}
