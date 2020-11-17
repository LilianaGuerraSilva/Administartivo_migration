using Galac.Saw.Brl.Inventario;
using Galac.Saw.Lib.Uil;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class CuadroDeBusquedaDeArticulosVerificadorViewModel : SearchBoxViewModelBase {
        const string CuadroDeBusquedaDeArticulosViewModelPropertyName = "CuadroDeBusquedaDeArticulosVerificadorViewModel";

        public CuadroDeBusquedaDeArticulosVerificadorViewModel() : base(CuadroDeBusquedaDeArticulosViewModelPropertyName, eTypeOfSearchInDb.Codigo_Descripcion) {
            DataService = new clsArticuloInventarioNav();
            IsControlVisible = false;
        }
    }
}
