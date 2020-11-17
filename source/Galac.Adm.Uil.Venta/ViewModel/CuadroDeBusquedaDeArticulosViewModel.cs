using Galac.Saw.Brl.Inventario;
using Galac.Saw.Lib.Uil;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CuadroDeBusquedaDeArticulosViewModel : SearchBoxViewModelBase {
        const string CuadroDeBusquedaDeArticulosViewModelPropertyName = "CuadroDeBusquedaDeArticulosViewModel";

        public CuadroDeBusquedaDeArticulosViewModel() : base(CuadroDeBusquedaDeArticulosViewModelPropertyName, eTypeOfSearchInDb.Codigo_Descripcion) {
            DataService = new clsArticuloInventarioNav();
            IsControlVisible = false;
        }
    }
}
