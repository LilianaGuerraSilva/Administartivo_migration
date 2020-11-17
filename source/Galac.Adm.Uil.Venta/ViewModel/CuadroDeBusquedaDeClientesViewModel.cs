using Galac.Saw.Brl.Cliente;
using Galac.Saw.Lib.Uil;
using LibGalac.Aos.UI.Mvvm.Validation;

namespace Galac.Adm.Uil.Venta.ViewModel {
    class CuadroDeBusquedaDeClientesViewModel : SearchBoxViewModelBase
    {
        const string CuadroDeBusquedaDeClientesViewModelPropertyName = "CuadroDeBusquedaDeClientesViewModel";

        public CuadroDeBusquedaDeClientesViewModel() : base(CuadroDeBusquedaDeClientesViewModelPropertyName, eTypeOfSearchInDb.RIF_Nombre) {
            DataService = new clsClienteNav();
            IsControlVisible = false;
        }

        public override void NotifyFocusAndSelect(string viewModelPropertyName = null) {
            base.NotifyFocusAndSelect("CuadroDeBusquedaDeArticulosViewModel");
        }

        [LibRequired(AllowEmptyStrings = false, ErrorMessage ="El campo Cliente es requerido")]
        public new string _filter {
            get { return _filter; }
            set {
                if (_filter != value) {
                    if (_filter == null) _filter = string.Empty;
                    CurrentPage = 1;
                    _filter = value;
                    RaisePropertyChanged("_filter");
                    LoadListAsync();
                    if (SelectedItem != null && SelectedItem.Description != value && !ListVisibility ||
                        SelectedItem == null && !ListVisibility) {
                        ShowList();
                    }
                        
                }
            }
        }
    }
}
