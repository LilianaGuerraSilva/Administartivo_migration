using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;

namespace Galac.Adm.Uil.GestionProduccion {
    public class clsListaDeMaterialesMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            ListaDeMaterialesMngViewModel vViewModel = new ListaDeMaterialesMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkListaDeMaterialesViewModel>("Lista de Materiales", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsListaDeMaterialesNav());
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesMenu

} //End of namespace Galac.Adm.Uil.GestionProduccion

