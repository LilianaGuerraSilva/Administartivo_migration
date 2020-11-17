using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.GestionCompras {
    public class clsCargaInicialMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            CargaInicialMngViewModel vViewModel = new CargaInicialMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (valUseInterop == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        //public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
        //    return LibFKRetrievalHelper.ChooseRecord<FkCargaInicialViewModel>("Costo Promedio", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsCargaInicialNav());
        //}
        #endregion //Metodos Generados


    } //End of class clsCargaInicialMenu

} //End of namespace Galac.Adm.Uil.GestionCompras

