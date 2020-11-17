using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Vehiculo;
using Galac.Saw.Uil.Vehiculo.ViewModel;
using Galac.Saw.Brl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo {
    public class clsMarcaMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            MarcaMngViewModel vViewModel = new MarcaMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkMarcaViewModel>("Marca", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsMarcaNav());
        }
        #endregion //Metodos Generados


    } //End of class clsMarcaMenu

} //End of namespace Galac.Saw.Uil.Vehiculo

