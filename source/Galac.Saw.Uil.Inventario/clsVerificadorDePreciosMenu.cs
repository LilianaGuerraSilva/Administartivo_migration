using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.Inventario {
    public class clsVerificadorDePreciosMenu : ILibMenu {
        public void Ejecuta(eAccionSR valAction, int handler) {
            var vViewModel = new VerificadorDePreciosMngViewModel();
            vViewModel.InitializeViewModel();
            vViewModel.RequestLoginAtClosing = true;
            LibMessages.EditViewModel.ShowTopmostEditor(vViewModel);
        }
    }
}
