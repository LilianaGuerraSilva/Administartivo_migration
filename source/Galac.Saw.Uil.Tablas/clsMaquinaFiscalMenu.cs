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
    public class clsMaquinaFiscalMenu : ILibMenu {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsMaquinaFiscalMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenu.Ejecuta(eAccionSR valAction, int handler) {
            MaquinaFiscalMngViewModel vViewModel = new MaquinaFiscalMngViewModel();
            vViewModel.ExecuteSearchAndInitLookAndFeel();
            LibSearchView insFrmSearch = new LibSearchView(vViewModel);
            if (handler == 0) {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            } else {
                insFrmSearch.ShowDialog();
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkMaquinaFiscalViewModel>("Impresora Fiscal", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsMaquinaFiscalNav());
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsMaquinaFiscalMenu

} //End of namespace Galac.Saw.Uil.Tablas

