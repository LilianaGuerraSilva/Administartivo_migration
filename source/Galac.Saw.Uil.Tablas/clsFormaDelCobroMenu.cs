using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Uil.Tablas.ViewModel;
using Galac.Saw.Brl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    public class clsFormaDelCobroMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            FormaDelCobroMngViewModel vViewModel = new FormaDelCobroMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkFormaDelCobroViewModel>("Forma Del Cobro", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsFormaDelCobroNav());
        }
        #endregion //Metodos Generados


    } //End of class clsFormaDelCobroMenu

} //End of namespace Galac.Saw.Uil.Tablas

