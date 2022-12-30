using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Vendedor;
using Galac.Adm.Brl.Vendedor;
using Galac.Adm.Uil.Vendedor.ViewModel;

namespace Galac.Adm.Uil.Vendedor {
    public class clsVendedorMenu: ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            VendedorMngViewModel vViewModel = new VendedorMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkVendedorViewModel>("Vendedor", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsVendedorNav());
        }
        #endregion //Metodos Generados


    } //End of class clsVendedorMenu

} //End of namespace Galac.Adm.Uil.Vendedor

