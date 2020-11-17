using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Uil.Banco.ViewModel;
using Galac.Adm.Brl.Banco;

namespace Galac.Adm.Uil.Banco {
    public class clsConceptoBancarioMenu: ILibMenu {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsConceptoBancarioMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenu
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            ConceptoBancarioMngViewModel vViewModel = new ConceptoBancarioMngViewModel();
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
            return LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsConceptoBancarioNav());
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsConceptoBancarioMenu

} //End of namespace Galac.Adm.Uil.Banco

