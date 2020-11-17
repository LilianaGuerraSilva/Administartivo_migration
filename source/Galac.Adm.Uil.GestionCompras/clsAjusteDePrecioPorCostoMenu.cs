using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.GestionCompras {
    public class clsAjusteDePrecioPorCostoMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta( eAccionSR valAction, int valUseInterop ) {
            AjusteDePrecioPorCostosViewModel vViewModel = new AjusteDePrecioPorCostosViewModel();
            vViewModel.ExecuteShowCommand();
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return true;
        }
        #endregion //Metodos Generados
    } //End of class clsLibroDeComprasMenu

} //End of namespace Galac.Adm.Uil.Compras

