using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica;

namespace Galac.Adm.Uil.DispositivosExternos {
    public class clsBalanzaMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            BalanzaMngViewModel vViewModel = new BalanzaMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkBalanzaViewModel>("Balanza", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsBalanzaNav());
        }
        #endregion //Metodos Generados


    } //End of class clsBalanzaMenu

} //End of namespace Galac.Adm.Uil.DispositivosExternos

