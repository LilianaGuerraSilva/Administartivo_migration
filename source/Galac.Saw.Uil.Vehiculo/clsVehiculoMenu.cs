using System.Collections.Generic;
using System.Xml;
using Galac.Saw.Brl.Vehiculo;
using Galac.Saw.Uil.Vehiculo.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;


namespace Galac.Saw.Uil.Vehiculo {
    public class clsVehiculoMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            VehiculoMngViewModel vViewModel = new VehiculoMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkVehiculoViewModel>("Vehículo", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsVehiculoNav());
        }
        #endregion //Metodos Generados

    } //End of class clsVehiculoMenu

} //End of namespace Galac.Saw.Uil.Vehiculo

